using MongoDB.Driver;
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

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for UserApproval.xaml
    /// </summary>
    public partial class UserApproval : UserControl
    {

        private readonly MongoDbConnection _connection;
        private readonly IMongoCollection<UsersModel> _userCollection;
        public UserApproval()
        {
            InitializeComponent();
          
            _connection = new MongoDbConnection(); // Initialize connection
            _userCollection = _connection.GetUsersCollection();  // Get the "Users" collection from the database
            LoadUsers();

        }

        private void LoadUsers()
        {
            var pendingUsers = _userCollection.AsQueryable()
                                              .Where(u => u.ApprovalStatus != "Approved")
                                              .ToList();
            ListViewUsers.ItemsSource = pendingUsers;  // Bind the pending users to the ListView

            var approvedUsers = _userCollection.AsQueryable()
                                               .Where(u => u.ApprovalStatus == "Approved")
                                               .ToList();
            ListViewApproved.ItemsSource = approvedUsers;
        }

        // Handle the Approve button click
        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.DataContext as UsersModel; // Get the user associated with the button clicked

            if (user != null)
            {
                user.ApprovalStatus = "Approved"; // Change the status to "Approved"
                _userCollection.ReplaceOne(u => u.Id == user.Id, user); // Update in the database

                MessageBox.Show("User approved successfully.");
                LoadUsers(); // Reload the list to reflect changes
            }
        }

        // Handle the Reject button click (Delete the user)
        private void RejectButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button?.DataContext as UsersModel; // Get the user associated with the button clicked

            if (user != null)
            {
                _userCollection.DeleteOne(u => u.Id == user.Id); // Delete from the database

                MessageBox.Show("User rejected and deleted.");
                LoadUsers(); // Reload the list to reflect changes
            }
        }

        private void ListViewUsers_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridView = (GridView)ListViewUsers.View;
            double totalWidth = ListViewUsers.ActualWidth;
            double columnWidth = totalWidth / gridView.Columns.Count;

            foreach (var column in gridView.Columns)
            {
                column.Width = columnWidth;
            }
        }

        private void ListViewApproved_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var gridView = (GridView)ListViewApproved.View;
            if (gridView != null)
            {
                double totalWidth = ListViewApproved.ActualWidth;

                // Assuming equal distribution of columns
                double columnWidth = totalWidth / gridView.Columns.Count;

                foreach (var column in gridView.Columns)
                {
                    column.Width = columnWidth;
                }
            }
        }
    }
}
