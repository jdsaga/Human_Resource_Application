using Microsoft.Win32;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Edit_Form.xaml
    /// </summary>
    public partial class EditForm : UserControl
    {
        private readonly MongoDbConnection _connection;
        private DateTime? _selectedBirthDate; // Declaring this field for the calender function
        private DateTime? _selectedHiredDate;
        private byte[] _uploadedImageBytes;
        private PeoplesModel _currentItem; // Store the current item being edited


        public EditForm(PeoplesModel selectedItem)
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            _currentItem = selectedItem;

            // Set the DataContext to the selected PeoplesModel
            this.DataContext = selectedItem;
            // Load the initial radio button status based on MongoDB
            LoadInitialStatus();
        }
        private void LoadInitialStatus()
        {
            // Ensure the current item's status is reflected in the radio buttons
            if (_currentItem != null)
            {
                if (_currentItem.Status == "Active")
                {
                    ActiveRadioButton.IsChecked = true;
                }
                else if (_currentItem.Status == "Inactive")
                {
                    InactiveRadioButton.IsChecked = true;
                }
            }
        }

        private async void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            if (_currentItem == null) return;
            var selectedRadioButton = sender as System.Windows.Controls.RadioButton;
            if (selectedRadioButton != null)
            {
                _currentItem.Status = selectedRadioButton.Content.ToString();
            }
        }

        private void ShowCalendar_Click(object sender, RoutedEventArgs e)
        {
            CalendarPopup.IsOpen = !CalendarPopup.IsOpen;
        }

        private void ShowDateHired_Click(object sender, RoutedEventArgs e)
        {

            HiredDatePopup.IsOpen = !HiredDatePopup.IsOpen;

        }

        // Set the selected date in the TextBox when a date is selected
        private void BirthDateCalendar_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (BirthDateCalendar.SelectedDate.HasValue)
            {
                _selectedBirthDate = BirthDateCalendar.SelectedDate.Value;
                BirthDateTextBox.Text = BirthDateCalendar.SelectedDate.Value.ToString("yyyy-MM-dd");

            }

            // Close the popup when a date is selected
            CalendarPopup.IsOpen = false;
        }

        private void Hired_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HiredDate.SelectedDate.HasValue)
            {
                _selectedHiredDate = HiredDate.SelectedDate.Value;
                HiredDateTextBox.Text = HiredDate.SelectedDate.Value.ToString("yyyy-MM-dd");
            }

            // Close the popup when a date is selected
            HiredDatePopup.IsOpen = false;
        }

        
        
        private void FormUploadButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select an image
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg;*.bmp)|*.png;*.jpg;*.jpeg;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Load the selected image into the Image control
                BitmapImage bitmap = new BitmapImage(new System.Uri(openFileDialog.FileName));

                // Set the source of the Image control to the loaded image
                UploadedImage.Source = bitmap;

                // Convert the image to a byte array
                using (MemoryStream stream = new MemoryStream())
                {
                    BitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(bitmap));
                    encoder.Save(stream);
                    _uploadedImageBytes = stream.ToArray(); // Store the byte array
                }
            }
        }

        private void ConfirmApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var item = this.DataContext as PeoplesModel;

                if ( item == null)
                {
                    MessageBox.Show("No item selected for editing.");
                   
                    return;
                }

                // Collect data from TextBoxes and ComboBoxes
                var firstName = ApplicantsFirstName.Text;
                var surname = ApplicantsSurname.Text;
                var middleName = ApplicantsMiddleName.Text;
                var age = ApplicantsAge.Text;
                var email = ApplicantsEmail.Text;
                var address = ApplicantsAddress.Text;
                var contactNo = ApplicantsContactNo.Text;
                var shuttleCode = ApplicantsShuttleCode.Text;
                var emergencyName = EmergencyContactName.Text;
                var emergencySurname = EmergencyContactSurname.Text;
                var emergencyMiddleName = EmergencyContactMiddleName.Text;
                var emergencyContact = EmergencyContactNo.Text;
                var emergencyAddress = EmergencyContactAddress.Text;

                // String containers for ComboBox selections
                string selectedSex = null;
                string selectedRequirements = null;
                string selectedEmergencyContactsSex = null;
                

                var dateofBirth = _selectedBirthDate ?? item.Birthday;
                var dateHired = _selectedHiredDate ?? item.DateHired;

                // Get ComboBox selections
                if (ApplicantsSex.SelectedItem is ComboBoxItem sexItem)
                {
                    selectedSex = sexItem.Content as string;
                }

                if (ApplicantsRequirements.SelectedItem is ComboBoxItem requirementsItem)
                {
                    selectedRequirements = requirementsItem.Content as string;
                }

                if (EmergencyContactSex.SelectedItem is ComboBoxItem emergencyItem)
                {
                    selectedEmergencyContactsSex = emergencyItem.Content as string;
                }

               

                // Validation checks
                if (string.IsNullOrEmpty(selectedSex) ||
                    string.IsNullOrEmpty(selectedRequirements) ||
                    string.IsNullOrEmpty(selectedEmergencyContactsSex))
                {
                    MessageBox.Show("Please select all required fields from the ComboBoxes.");
                    return;
                }

                if (new[] { firstName, surname, middleName, age, email, address, contactNo, shuttleCode, emergencyName, emergencySurname, emergencyMiddleName, emergencyContact, emergencyAddress }
                    .Any(string.IsNullOrWhiteSpace))
                {
                    MessageBox.Show("Fill up all the fields.");
                    return;
                }

                // Get the collection
                var peoplesCollection = _connection.GetPeoplesCollection();

                // Build a filter to find the document by ID
                var filter = Builders<PeoplesModel>.Filter.Eq(p => p.Id, item.Id);

                // Build the update definition
                var update = Builders<PeoplesModel>.Update
                    .Set(p => p.FirstName, firstName)
                    .Set(p => p.Surname, surname)
                    .Set(p => p.MiddleName, middleName)
                    .Set(p => p.Age, age)
                    .Set(p => p.Email, email)
                    .Set(p => p.Address, address)
                    .Set(p => p.ContactNo, contactNo)
                    .Set(p => p.ShuttleCode, shuttleCode)
                    .Set(p => p.ContactsFirstName, emergencyName)
                    .Set(p => p.ContactsSurname, emergencySurname)
                    .Set(p => p.ContactsMiddleName, emergencyMiddleName)
                    .Set(p => p.ContactsNo, emergencyContact)
                    .Set(p => p.ContactsAddress, emergencyAddress)
                    .Set(p => p.Sex, selectedSex)
                    .Set(p => p.Requirements, selectedRequirements)
                    .Set(p => p.ContactsSex, selectedEmergencyContactsSex)
                    .Set(p => p.Birthday, dateofBirth)
                    .Set(p => p.DateHired, dateHired)
                    .Set(p => p.ProfileImage, _uploadedImageBytes ?? item.ProfileImage)
                    .Set(p => p.Status, _currentItem.Status);

                // Perform the update
                var result = peoplesCollection.UpdateOne(filter, update);

                // Check if the update was successful
                if (result.ModifiedCount > 0)
                {
                    MessageBox.Show("Person updated successfully!");
                }
                else
                {
                    MessageBox.Show("No changes made.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating user: {ex.Message}");
            }
        }

       

    }
}
