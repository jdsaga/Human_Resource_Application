using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for ForgotPass.xaml
    /// </summary>
    public partial class ForgotPass : UserControl
    {
        private readonly MongoDbConnection _connection;
        private string _otp; // To store the generated OTP
        public ForgotPass()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            LogoImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/realcompanylogo.png"));
            BackgroundPicture.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/wallpaper.jpg"));

        }

        //Generates otp
        private string GenerateOTP()
        {
            Random random = new Random();
            string otp = random.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
            return otp;
        }

        //Encrypts password
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }

        private void SendOTPEmail(string email, string otp)
        {
            try
            {

                //setup the mail sender

                var mailMessage = new MailMessage();
                mailMessage.From = new MailAddress("sc.christianjericho.sanagustin@cvsu.edu.ph"); // Replace with your email address
                mailMessage.To.Add(email);
                mailMessage.Subject = "Your OTP for password change";
                mailMessage.Body = $"Your OTP is: {otp}";

                // Set up the SMTP client
                var smtpClient = new SmtpClient("smtp.gmail.com") // Replace with your SMTP server
                {
                    Port = 587,
                    Credentials = new NetworkCredential("sc.christianjericho.sanagustin@cvsu.edu.ph", "lcjcsccxprfyojuk"), // Replace with your credentials
                    EnableSsl = true
                };

                smtpClient.Send(mailMessage);

            }
            catch (Exception ex) {

                MessageBox.Show($"An error occurred while sending the OTP: {ex.Message}");
            }

        }

        private void Send_OTPBtn(object sender, RoutedEventArgs e)
        {
            //email entered by the user
            var enteredEmail = emailTextBox.Text;

            if (string.IsNullOrEmpty(enteredEmail))
            {
                MessageBox.Show("Please enter an email address.");
                return;
            }
            try
            {
                // Check if the email exists in the database
                var userCollection = _connection.GetUsersCollection(); // Assumes this method returns the users collection
                var emailExists = userCollection.Find(u => u.Email == enteredEmail).Any();

                if (!emailExists)
                {
                    MessageBox.Show("The email address is not registered in the system.");
                    return;
                }

                // Generate the OTP
                _otp = GenerateOTP();

                // Send the OTP to the user's email
                SendOTPEmail(enteredEmail, _otp);
                MessageBox.Show("OTP has been sent to your email address.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }

        private void BackForgotPass_Click(object sender, RoutedEventArgs e)
        {
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }

        private void SaveForgotPass_Click(object sender, RoutedEventArgs e)
        {
            var otpInput = otpTextbox.Text;
            var newPassword = newpasswordTextBox.Password;
            var confirmNewPass = confirmNewPassword.Password;
            var email = emailTextBox.Text;

            // Checks OTP if correct
            if (string.IsNullOrEmpty(otpInput) || otpInput != _otp)
            {
                MessageBox.Show("Invalid or missing OTP. Please try again.");
                return;
            }

            // Check if the password is valid
            if (string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("New password cannot be empty.");
                return;
            }

            if (newPassword != confirmNewPass)
            {
                MessageBox.Show("New password and confirm new password do not match.");
                return;
            }

            var hashedPassword = HashPassword(newPassword);

            try
            {
                var userCollection = _connection.GetUsersCollection();
                var filter = Builders<UsersModel>.Filter.Eq(u => u.Email, email);
                var update = Builders<UsersModel>.Update.Set(u => u.Password, hashedPassword);

                // Execute the update
                var result = userCollection.UpdateOne(filter, update);

                if (result.ModifiedCount > 0)
                {
                    MessageBox.Show("Password updated successfully.");
                }
                else
                {
                    MessageBox.Show("No user found with this email.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error changing password: {ex.Message}");
                return;
            }

            // Optionally redirect to login page
            var loginandsignup = (LoginAndSignup)Application.Current.MainWindow;
            loginandsignup.LoginHyperlink();
        }



    }
}
