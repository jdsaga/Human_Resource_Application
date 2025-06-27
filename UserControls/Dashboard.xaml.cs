using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using MongoDB.Driver;
using SharpVectors.Dom.Events;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : UserControl
    {
        private readonly MongoDbConnection _connection;
        private DispatcherTimer _updateTimer; // Simplified to DispatcherTimer
        private List<Event> _events;
        private bool isProcessing = false;
        public Dashboard()
        {

            InitializeComponent();
            SetCurrentDate();
            _connection = new MongoDbConnection();
            LoadData();
            HolidayEvents(); // Ensure events are initialized
            StartUpdateTimer();



        }

        private void SetCurrentDate()
        {
            // Set the current date on the Calendar
            RealTimeCalendar.SelectedDate = DateTime.Now;
            RealTimeCalendar.DisplayDate = DateTime.Now;
        }

        private void LoadData()
        {
           var peoplesCollection = _connection.GetPeoplesCollection();
            List<PeoplesModel> users = peoplesCollection.Find(FilterDefinition<PeoplesModel>.Empty).ToList();
            ListViewUsers.ItemsSource = users;
        }

        public ICommand EditItemCommand { get; set; }


        private void SetupListViewEvents()
        {
            ListViewUsers.MouseDoubleClick += ListViewUsers_MouseDoubleClick;
        }

        private void ListViewUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Check if the double-click originated from a CheckBox
            if (e.OriginalSource is CheckBox)
            {
                e.Handled = true; // Suppress the double-click event
            }
            if (ListViewUsers.SelectedItem is PeoplesModel selectedItem)
            {
                // Fetch the full document from the database
                var peoplesCollection = _connection.GetPeoplesCollection();
                var detailedItem = peoplesCollection.Find(x => x.Id == selectedItem.Id).FirstOrDefault();

                if (detailedItem != null)
                {
                    // Pass the full PeoplesModel to EditForm
                    EditForm editForm = new EditForm(detailedItem);

                    
                    // Show the EditForm window
                    Window editWindow = new Window
                    {
                        Content = editForm,
                        Title = "Edit Item",
                        Width = 1200,
                        Height = 600
                    };

                    // Register for the Closed event of the EditForm window
                    editWindow.Closed += (s, args) => RefreshData();

                    editWindow.Show();
                }
                else
                {
                    MessageBox.Show("Failed to fetch item details.");
                }
            }
            else
            {
                MessageBox.Show("Please select an item to edit.");
            }
        }
        private void ListViewUsers_PreviewMouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            // Check if the double-click originated from a CheckBox
            if (e.OriginalSource is CheckBox)
            {
                e.Handled = true; // Suppress the double-click event
            }
        }






        private void HolidayEvents()
        {
            // Example events (replace with your data source)
            _events = new List<Event>
    {
        new Event { Name = "Valentine's Day", Date = new DateTime(DateTime.Now.Year, 2, 14) },
        new Event { Name = "Christmas", Date = new DateTime(DateTime.Now.Year, 12, 25) },
        new Event { Name = "Halloween", Date = new DateTime(DateTime.Now.Year, 10, 31) },
        new Event { Name = "New Year", Date = new DateTime(DateTime.Now.Year, 1, 1) },
        new Event { Name = "Lunar New Year", Date = new DateTime(DateTime.Now.Year, 1, 29) },
        new Event { Name = "Labour Day", Date = new DateTime(DateTime.Now.Year, 5, 1) },
        new Event { Name = "Holy Saturday", Date = new DateTime(DateTime.Now.Year, 4, 19) },
      
    };
        }

        private void StartUpdateTimer()
        {
            _updateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            _updateTimer.Tick += UpdateNotes;
            _updateTimer.Start();
        }

        private void UpdateNotes(object sender, EventArgs e)
        {
            if (_events == null)
            {
                NotesTextBox.Text = "No events available.";
                return;
            }

            DateTime now = DateTime.Now;
            DateTime today = now.Date;
            DateTime rangeEnd = today.AddDays(60);

            // Adjust event dates to the current or next year for recurring events
            var adjustedEvents = _events.Select(ev =>
            {
                DateTime adjustedDate = new DateTime(today.Year, ev.Date.Month, ev.Date.Day, ev.Date.Hour, ev.Date.Minute, ev.Date.Second);

                // If the event has already passed this year, move it to next year
                if (adjustedDate < now)
                {
                    adjustedDate = adjustedDate.AddYears(1);
                }

                return new Event { Name = ev.Name, Date = adjustedDate };
            }).ToList();

            // Filter events within the next 60 days
            var upcomingEvents = adjustedEvents
                .Where(ev => ev.Date >= now && ev.Date <= rangeEnd)
                .OrderBy(ev => ev.Date)
                .ToList();

            // Update the NotesTextBox
            NotesTextBox.Text = upcomingEvents.Any()
                ? string.Join(Environment.NewLine, upcomingEvents.Select(ev =>
                {
                    // Display time for today's events
                    if (ev.Date.Date == today)
                        return $"{ev.Date:MMMM dd, h:mm tt}: {ev.Name}";
                    else
                        return $"{ev.Date:MMMM dd}: {ev.Name}";
                }))
                : "No upcoming events.";
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the button that triggered the event
            Button button = sender as Button;

            // Get the associated data context (the clicked item)
            if (button?.DataContext is PeoplesModel selectedItem)
            {
                // Confirm deletion
                var result = MessageBox.Show($"Are you sure you want to delete {selectedItem.FirstName}?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Delete from MongoDB
                        var peoplesCollection = _connection.GetPeoplesCollection();
                        var deleteResult = peoplesCollection.DeleteOne(x => x.Id == selectedItem.Id);

                        // Check if deletion was successful
                        if (deleteResult.DeletedCount > 0)
                        {
                            MessageBox.Show($"{selectedItem.FirstName} was deleted successfully.", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                            // Refresh the ListView
                            RefreshData();
                        }
                        else
                        {
                            MessageBox.Show($"Failed to delete {selectedItem.FirstName}.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }


        public void RefreshData()
        {
            LoadData();
        }


    }

    public class Event
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
    



}
