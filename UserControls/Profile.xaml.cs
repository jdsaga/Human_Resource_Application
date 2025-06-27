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
using MongoDB.Bson;
using System.IO;
using System.Data.Common;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Profile.xaml
    /// </summary>
    public partial class Profile : UserControl
    {
        private readonly MongoDbConnection _connection;
        private readonly string PassedUsername;
        public Profile(string text)
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            PassedUsername = text;
            LoadProfileImage();


        }

        private void LoadProfileImage()
        {
            var username = PassedUsername;
            var userCollection = _connection.GetUsersCollection();

            var filter = Builders<UsersModel>.Filter.Eq(u => u.Username, username);
            var user = userCollection.Find(filter).FirstOrDefault();

            if (user != null && user.ProfileImage != null)
            {
                try
                {
                    // Convert byte array back to BitmapImage
                    var imageStream = new MemoryStream(user.ProfileImage);
                    var bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = imageStream;
                    bitmap.EndInit();

                    // Set the image as the source for the ProfileImage control
                    ProfileImage.Source = bitmap;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Profile image not found.");
            }
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    // Get the selected image path
                    var imagePath = openFileDialog.FileName;

                    // Load the image into the Image control for display
                    ProfileImage.Source = new BitmapImage(new Uri(imagePath));

                    // Convert the image into a byte array
                    byte[] imageBytes = File.ReadAllBytes(imagePath);
                    // Store the image in MongoDB
                    var username = PassedUsername;
                    var userCollection = _connection.GetUsersCollection();

                    // Create the filter to find the user by username
                    var filter = Builders<UsersModel>.Filter.Eq(u => u.Username, username);

                    // Create the update definition to set the profile image
                    var update = Builders<UsersModel>.Update.Set(u => u.ProfileImage, imageBytes);

                    // Update the user's profile image in MongoDB
                    var result = userCollection.UpdateOne(filter, update);

                    if (result.ModifiedCount > 0)
                    {
                        MessageBox.Show("Image uploaded and updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("Image upload failed. No records were updated.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    MessageBox.Show("Image uploaded successfully.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error uploading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.IsReadOnly = false;
            LastNameTextbox.IsReadOnly = false;
            MiddleNameTextBox.IsReadOnly = false;
            ContactNoTextBox.IsReadOnly = false;
            EmailTextBox.IsReadOnly = false;

            EditButton.Visibility = Visibility.Collapsed;
            CancelButton.Visibility = Visibility.Visible;


        }

        private void CancelEditButton_Click(object sender, RoutedEventArgs e)
        {
            FirstNameTextBox.IsReadOnly = true;
            LastNameTextbox.IsReadOnly = true;
            MiddleNameTextBox.IsReadOnly = true;
            ContactNoTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;

            EditButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Collapsed;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            var username = PassedUsername;
            var firstName = FirstNameTextBox.Text;
            var lastName = LastNameTextbox.Text;
            var middleName = MiddleNameTextBox.Text;
            var contactNo = ContactNoTextBox.Text;
            var email = EmailTextBox.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(middleName) || string.IsNullOrWhiteSpace(contactNo) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Fill up all the fields.");
                return;
            }

            var userCollection = _connection.GetUsersCollection();

            var filter = Builders<UsersModel>.Filter.Eq(u => u.Username, username);

            var update = Builders<UsersModel>.Update
                .Set(u => u.FirstName, firstName)
                .Set(u => u.LastName, lastName)
                .Set(u => u.MiddleName, middleName)
                .Set(u => u.ContactNo, contactNo)
                .Set(u => u.Email, email);
  
            userCollection.UpdateOne(filter, update);

            MessageBox.Show("User details updated successfully.");

            FirstNameTextBox.IsReadOnly = true;
            LastNameTextbox.IsReadOnly = true;
            MiddleNameTextBox.IsReadOnly = true;
            ContactNoTextBox.IsReadOnly = true;
            EmailTextBox.IsReadOnly = true;

            EditButton.Visibility = Visibility.Visible;
            CancelButton.Visibility = Visibility.Collapsed;


        }


    }
}
