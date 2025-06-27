using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MongoDB.Driver;
using System.IO;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for ApplicationForm.xaml
    /// </summary>
    public partial class ApplicationForm : UserControl
    {
        private readonly MongoDbConnection _connection;
        private DateTime? _selectedBirthDate; // Declaring this field for the calender function
        private DateTime? _selectedHiredDate;
        private byte[] _uploadedImageBytes;
        private byte[] _applicantsSignature;
        private byte[] _authorizeSignature;
        public ApplicationForm()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            Signature signature = new Signature();
            BirthDateTextBox.IsReadOnly = true;
            HiredDateTextBox.IsReadOnly = true;
            Calendaricon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Calendar-Icon.png"));
            Calendaricon1.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Calendar-icon.png"));


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

        

        private void OpenSignature_Click(object sender, RoutedEventArgs e)
        {
            Signature signature = new Signature();
            signature.SignatureType = "Applicants";

            Window hostWindow = new Window
            {
                Content = signature,
                Width = 800,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Show the signature window modally, so the main window waits until it is closed
            hostWindow.ShowDialog(); // Wait until the user closes the window

            // After the signature window is closed, we retrieve the signature data
            if (signature._applicantsSignature != null)
            {
                _applicantsSignature = signature._applicantsSignature; // Store the signature byte array in the main window
            }
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

        private void AuthoritySig_Click(object sender, RoutedEventArgs e)
        {
            // Create the signature control
            Signature signature = new Signature();
            signature.SignatureType = "Authorization"; // Set the signature type

            // Create and show the signature window modally
            Window hostWindow = new Window
            {
                Content = signature,
                Width = 800,
                Height = 500,
                WindowStartupLocation = WindowStartupLocation.CenterScreen
            };

            // Show the window modally so the main window waits for it to close
            hostWindow.ShowDialog();

            // After the signature window is closed, retrieve the authorization signature data
            if (signature._authorizeSignature != null)
            {
                _authorizeSignature = signature._authorizeSignature; // Store the authorization signature in the main window
            }
           
        }


        private void ConfirmApplicationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_applicantsSignature == null)
                {
                    MessageBox.Show("Applicant signature is missing.");
                    return; // Prevent insertion if signature is missing
                }

                if (_authorizeSignature == null)
                {
                    MessageBox.Show("Authorization signature is missing.");
                    return; // Prevent insertion if authorization signature is missing
                }
                // Collects data from TextBoxes
                var firstName = ApplicantsFirstName.Text;
                var surname = ApplicantsSurname.Text;
                var middleName = ApplicantsMiddleName.Text;
                var ageText = ApplicantsAge.Text;
                var email = ApplicantsEmail.Text;
                var address = ApplicantsAddress.Text;
                var contactNo = ApplicantsContactNo.Text;
                var shuttleCode = ApplicantsShuttleCode.Text;
                var emergencyName = EmergencyContactName.Text;
                var emergencySurname = EmergencyContactSurname.Text;
                var emergencyMiddleName = EmergencyContactMiddleName.Text;
                var emergencyContact = EmergencyContactNo.Text;
                var emergencyAddress = EmergencyContactAddress.Text;

                // String container where the data from ComboBoxes will be stored
                string selectedSex = null;
                string selectedRequirements = null;
                string selectedEmergencyContactsSex = null;
               

                // Date validation
                var dateofBirth = _selectedBirthDate.Value;
                var dateHired = _selectedHiredDate.Value;

                // Takes the item from ComboBoxes and adds to string containers
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

               

                // Validate ComboBox selections
                if (string.IsNullOrEmpty(selectedSex))
                {
                    MessageBox.Show("Please select a sex.");
                    return;
                }

                if (string.IsNullOrEmpty(selectedRequirements))
                {
                    MessageBox.Show("Please select requirements info.");
                    return;
                }

                if (string.IsNullOrEmpty(selectedEmergencyContactsSex))
                {
                    MessageBox.Show("Please select a sex for the emergency contact.");
                    return;
                }

               

                // Validate TextBox inputs
                if (new[] { firstName, surname, middleName, ageText, email, address, contactNo, shuttleCode, emergencyName, emergencySurname, emergencyMiddleName, emergencyContact, emergencyAddress }.Any(string.IsNullOrWhiteSpace))
                {
                    MessageBox.Show("Fill up all the fields.");
                    return;
                }

                // Validate selected date
                if (!_selectedBirthDate.HasValue)
                {
                    MessageBox.Show("Please select a Date of Birth.");
                    return;
                }

                if (!_selectedHiredDate.HasValue)
                {
                    MessageBox.Show("Please select a Date Hired.");
                    return;
                }

                // Validate Age matches Date of Birth
                if (int.TryParse(ageText, out int age))
                {
                    var calculatedAge = DateTime.Now.Year - dateofBirth.Year;
                    if (DateTime.Now.DayOfYear < dateofBirth.DayOfYear)
                    {
                        calculatedAge--; // Adjust if birthday hasn't occurred yet this year
                    }

                    if (calculatedAge != age)
                    {
                        MessageBox.Show("The age does not match the Date of Birth.");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid age input.");
                    return;
                }


                var peoplesCollection = _connection.GetPeoplesCollection();

                // Generate ID
                string currentYear = dateHired.Year.ToString(); // Extract hiring year
                var latestPerson = peoplesCollection
                    .Find(_ => true)
                    .SortByDescending(p => p.EmployeeId) // Sort by EmployeeId
                    .FirstOrDefault();

                string newEmployeeId;
                if (latestPerson != null && !string.IsNullOrEmpty(latestPerson.EmployeeId))
                {
                    // Increment the sequential part
                    var lastSequential = int.Parse(latestPerson.EmployeeId.Split('-')[2]);
                    newEmployeeId = $"{currentYear}-EMP-{(lastSequential + 1):D2}";
                }
                else
                {
                    // Start with 01 for the new year
                    newEmployeeId = $"{currentYear}-EMP-01";
                }

                var newPerson = new PeoplesModel
                {
                    EmployeeId = newEmployeeId, // Assign the new ID
                    FirstName = firstName,
                    Surname = surname,
                    MiddleName = middleName,
                    Age = ageText,
                    Email = email,
                    Address = address,
                    ContactNo = contactNo,
                    ShuttleCode = shuttleCode,
                    ContactsFirstName = emergencyName,
                    ContactsSurname = emergencySurname,
                    ContactsMiddleName = emergencyMiddleName,
                    ContactsNo = emergencyContact,
                    ContactsAddress = emergencyAddress,
                    Sex = selectedSex,
                    Requirements = selectedRequirements,
                    ContactsSex = selectedEmergencyContactsSex,
                    Birthday = dateofBirth,
                    DateHired = dateHired,
                    ProfileImage = _uploadedImageBytes,
                    ApplicantSignature = _applicantsSignature,
                    AuthorizeSignature = _authorizeSignature,
                    
                };

                peoplesCollection.InsertOne(newPerson);

                MessageBox.Show($"Person added successfully with ID: {newEmployeeId}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
                return;
            }

        }

        private void Number_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "1234567890";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }

        //Set what inputs can only be added in the username textboxes
        private void Character_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ-";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }
    }
}
