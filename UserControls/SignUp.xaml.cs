using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Windows.Media.Animation;


namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class SignUp : UserControl
    {
        private readonly MongoDbConnection _connection;
        public SignUp()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            BackgroundPicture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/wallpaper.jpg"));
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

        private void Username_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
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

        /*Function to ng hyperlink na para mapunta sa login. Bali trinitrigger nito yung function na nasa LoginAndSignup window para mahide yung signup na usercontrol at mashow yung login na usercontrol*/
        private void LoginHyperlink_Click(object sender, RoutedEventArgs e)
        {
          var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        // Function to validate the email
        private bool IsValidEmail(string email)
        {
            // Check if the email is empty
            if (string.IsNullOrWhiteSpace(email))
            {
                return false;
            }

            // Regular Expression for Email Format Validation
            string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            if (!Regex.IsMatch(email, emailPattern))
            {
                return false;
            }

            // Check if the email length is less than or equal to 100 characters
            if (email.Length > 100)
            {
                return false;
            }

            return true;
        }

        // Function to validate the contact number
        private bool IsValidContactNumber(string contactNumber)
        {
            // Check if the contact number is empty
            if (string.IsNullOrWhiteSpace(contactNumber))
            {
                return false;
            }

            // Regular Expression for US Phone Number Format Validation (e.g., (123) 456-7890 or 123-456-7890)
            string contactNumberPattern = @"^09\d{9}$"
;

            // Validate against the regular expression
            if (!Regex.IsMatch(contactNumber, contactNumberPattern))
            {
                return false;
            }

            // Optional: Check the length of the phone number (for U.S., should be 10 digits, or 11 including country code)
            if (contactNumber.Replace(" ", "").Replace("-", "").Replace("(", "").Replace(")", "").Length < 11)
            {
                return false;
            }

            return true;
        }
        // Function to validate the password
        private bool IsValidPassword(string password)
        {
            // Check if the password is empty
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            // Regular Expression for Password Validation:
            // - At least one uppercase letter
            // - At least one lowercase letter
            // - At least one number
            // - At least one special character (e.g., !@#$%^&)
            // - Minimum length of 8 characters, maximum length of 20 characters
            string passwordPattern = @"^(?=.[a-z])(?=.[A-Z])(?=.\d)(?=.[@$!%?&])[A-Za-z\d@$!%*?&]{8,20}$";

            // Validate against the regular expression
            if (!Regex.IsMatch(password, passwordPattern))
            {
                return false;
            }

            return true;
        }
        private void SignUpBtn_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                var firstName = SignUpFirstName.Text;
                var lastName = SignUpLastName.Text;
                var middleName = SignUpMiddleName.Text;
                var contactNo = SignUpContactNo.Text;
                var email = SignUpEmail.Text.Trim();
                var username = SignupUsername.Text;
                var password = SignupPassword.Password;
                var Cpassword = SignupCPassword.Password;

                // Validate the email
                if (!IsValidEmail(email))
                {
                    // Display error message
                    EmailErrorMessage.Text = "Please enter a valid email address.";
                    EmailErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    ContactNumberErrorMessage.Visibility = Visibility.Collapsed;
                }


                // Validate the contact number
                if (!IsValidContactNumber(contactNo))
                {
                    // Display error message
                    ContactNumberErrorMessage.Text = "Please enter a valid contact number.";
                    ContactNumberErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    ContactNumberErrorMessage.Visibility = Visibility.Collapsed;
                }


                if (string.IsNullOrWhiteSpace(firstName))
                {
                    FirstNameErrorMessage.Text = "First name cannot be empty.";
                    FirstNameErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    FirstNameErrorMessage.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrWhiteSpace(lastName))
                {
                    LastNameErrorMessage.Text = "Last name cannot be empty.";
                    LastNameErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    LastNameErrorMessage.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrWhiteSpace(middleName))
                {
                    MiddleNameErrorMessage.Text = "Middle name cannot be empty.";
                    MiddleNameErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    MiddleNameErrorMessage.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrWhiteSpace(username))
                {
                    UsernameErrorMessage.Text = "Username cannot be empty.";
                    UsernameErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    UsernameErrorMessage.Visibility = Visibility.Collapsed;
                }

                // Validate the contact number
                if (!IsValidPassword(password))
                {
                    PasswordErrorMessage.Inlines.Clear(); // Clear previous messages

                    PasswordErrorMessage.Inlines.Add(new Run("Password must contain at least one uppercase letter,"));
                    PasswordErrorMessage.Inlines.Add(new LineBreak());
                    PasswordErrorMessage.Inlines.Add(new Run("one lowercase letter, one number, and"));
                    PasswordErrorMessage.Inlines.Add(new LineBreak());
                    PasswordErrorMessage.Inlines.Add(new Run("one special character."));
                

                    PasswordErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    PasswordErrorMessage.Visibility = Visibility.Collapsed;
                }
                if (password != Cpassword || string.IsNullOrWhiteSpace(Cpassword))
                {
                    CPasswordErrorMessage.Text = "Passwords do not match.";
                    CPasswordErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    CPasswordErrorMessage.Visibility = Visibility.Collapsed;
                }


                var userCollection = _connection.GetUsersCollection();

                // Checks if user already exists.
                if (userCollection.AsQueryable().Any(u => u.Username == username))
                {
                    // Display error message
                    PasswordErrorMessage.Text = "Username already exist.";
                    PasswordErrorMessage.Visibility = Visibility.Visible;
                    MessageBox.Show("Username already exist.");
                    return;
                }
                else
                {
                    UsernameErrorMessage.Visibility = Visibility.Collapsed;
                }


                // Checks if email already exists.
                if (userCollection.AsQueryable().Any(u => u.Email == email))
                {
                    // Display error message
                    EmailErrorMessage.Text = "Email.";
                    PasswordErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                else
                {
                    EmailErrorMessage.Visibility = Visibility.Collapsed;
                }

                var hashedPassword = HashPassword(password);

                var newUser = new UsersModel {FirstName = firstName, LastName = lastName, MiddleName = middleName, ContactNo = contactNo, Email = email, Username = username, Password = hashedPassword, ApprovalStatus = "Pending"};
                userCollection.InsertOne(newUser);

                var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
                loginandsignup.LoginHyperlink();




            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding user: {ex.Message}");
                return;
            }
        
        }



        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void PasswordInfoIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            // Toggle the visibility of the password message when the icon is clicked
            if (PasswordErrorMessage.Visibility == Visibility.Collapsed)
            {
                PasswordErrorMessage.Visibility = Visibility.Visible;
            }
            else
            {
                PasswordErrorMessage.Visibility = Visibility.Collapsed;
            }
        }

        private void SignUpEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

            var email = SignUpEmail.Text.Trim();
            // Hide the error message while the user is typing
            EmailErrorMessage.Visibility = Visibility.Collapsed;

            // Real-time validation can be done here if needed
            if (!IsValidEmail(email))
            {
                EmailErrorMessage.Text = "Invalid email format.";
                EmailErrorMessage.Visibility = Visibility.Visible;
            }
        }

       
    }
}
