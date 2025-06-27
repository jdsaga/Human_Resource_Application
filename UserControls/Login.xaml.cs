using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Security.Cryptography;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private readonly MongoDbConnection _connection;

        public Login()
        {
            InitializeComponent();
            BackgroundPicture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/wallpaper.jpg"));
            _connection = new MongoDbConnection();
          
        }

        //Set what inputs can only be added in the username textboxes
        private void UsernameTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            // Allow only alphanumeric characters and some symbols
            string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789.-_";
            if (!allowedChars.Contains(e.Text))
            {
                e.Handled = true;  // Reject the input
            }
        }

        /*Function to ng hyperlink na para mapunta sa Signup. Bali trinitrigger nito yung function na nasa LoginAndSignup window para mahide yung login na usercontrol at mashow yung signup na usercontrol*/
        private void SignupHyperlink_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.SignupHyperlink();
        }

        /*Function para mabuksan yung HomeDesign na window (hindi pa final, for experimental lang para makita yung design */ 
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            
            

            var username = LoginTextBox.Text;
            var password = LoginPasswordBox.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) {
                MessageBox.Show("Fill up all the fields.");
                return;
            } 

            var userCollection = _connection.GetUsersCollection();

            var hashedPassword = HashPassword(password);

            var user = userCollection.AsQueryable().FirstOrDefault(u => u.Username == username && u.Password == hashedPassword);
            
            if (user != null )
            {
                if (user.ApprovalStatus != "Approved")
                {
                    MessageBox.Show("Your account has not been approved yet.");
                    return;
                }

                var homeDesign = new HomeDesign();
                homeDesign.DataContext = user;
                homeDesign.Show();
                LoginPasswordBox.Password = string.Empty;
                Window parentWindow = Window.GetWindow(this);
                parentWindow.Hide();
            }
            else
            {
                var checkUsername = userCollection.AsQueryable().FirstOrDefault(u => u.Username == username);
                var checkPassword = userCollection.AsQueryable().FirstOrDefault(u => u.Password == hashedPassword);

                if (string.IsNullOrWhiteSpace(username))
                {
                    UsernameErrorMessage.Text = "Please enter your username.";
                    UsernameErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                else if (checkUsername == null)
                {
                    UsernameErrorMessage.Text = "No user found with this username.";
                    UsernameErrorMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    UsernameErrorMessage.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrWhiteSpace(password))
                {
                    PasswordErrorMessage.Text = "Please enter your password.";
                    PasswordErrorMessage.Visibility = Visibility.Visible;
                    return;
                }
                else if (checkPassword == null)
                {
                    PasswordErrorMessage.Text = "Incorrect Password.";
                    PasswordErrorMessage.Visibility = Visibility.Visible;
                }
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
        private void TogglePasswordVisibility_Click(object sender, RoutedEventArgs e)
        {
            // Check if the password is currently hidden
            if (LoginPasswordBox.Visibility == Visibility.Visible)
            {
                // Show the password in the TextBox
                PasswordTextBox.Text = LoginPasswordBox.Password;
                PasswordTextBox.Visibility = Visibility.Visible;
                LoginPasswordBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Hide the password in the PasswordBox
                LoginPasswordBox.Password = PasswordTextBox.Text;
                LoginPasswordBox.Visibility = Visibility.Visible;
                PasswordTextBox.Visibility = Visibility.Collapsed;
            }
        }
        private void ForgotPassword_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.ForgotPassHyperlink();
        }

       
    }
}
