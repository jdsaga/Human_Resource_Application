using Human_Resources_Management_System.UserControls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using MongoDB.Driver;
using System.Windows.Media.Imaging;

namespace Human_Resources_Management_System
{
    public partial class HomeDesign : Window
    {
        private readonly MongoDbConnection _connection;
        private Button _selectedButton; // Track the currently selected button
        private Brush _defaultBackground = (Brush)new BrushConverter().ConvertFromString("#343030");

        public HomeDesign()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();

            realLogo.Source = new BitmapImage(new Uri("pack://application:,,,/Images/realcompanylogo.png"));
            DashBoardIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/OrangeDashboardIcon.png"));
            AppFormIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/OrangeApplicationIcon.png"));
            ViewProfileIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/OrangeProfileIcon.png"));
            PayrollIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/OrangeDashboardIcon.png"));
            ApprovalIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Approvallll-icon-NOBG.png"));
            SupportIcon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/OrangeSupportIcon.png"));

            // Set Dashboard as the default selected button
            ContentDisplay.Content = new Dashboard();
            SetSelectedButton(DashboardBtn);

            // Mouse event handlers
            DashboardBtn.MouseEnter += HoverButton;
            DashboardBtn.MouseLeave += LeaveButton;
            ApplicationFormBtn.MouseEnter += HoverButton;
            ApplicationFormBtn.MouseLeave += LeaveButton;
            ProfileBtn.MouseEnter += HoverButton;
            ProfileBtn.MouseLeave += LeaveButton;
            PayrollBtn.MouseEnter += HoverButton;
            PayrollBtn.MouseLeave += LeaveButton;
            SupportBtn.MouseEnter += HoverButton;
            SupportBtn.MouseLeave += LeaveButton;
            ExitBtn.MouseEnter += HoverButton;
            ExitBtn.MouseLeave += LeaveButton;
        }

        // Change the selected button's background and revert the previous one
        private void SetSelectedButton(Button btn)
        {
            if (_selectedButton != null)
            {
                _selectedButton.Background = _defaultBackground;
                _selectedButton.Foreground = Brushes.White;
            }

            _selectedButton = btn;
            _selectedButton.Background = Brushes.Black;
            _selectedButton.Foreground = Brushes.White;
        }

        // Hover effects
        private void HoverButton(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != _selectedButton)
            {
                btn.Foreground = Brushes.Black;
            }
        }

        private void LeaveButton(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn != _selectedButton)
            {
                btn.Background = _defaultBackground;
                btn.Foreground = Brushes.White;
            }
        }

        // Button click events
        private void DashboardBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new Dashboard();
            SetSelectedButton(DashboardBtn);
        }

        private void ApplicationFormBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new ApplicationForm();
            SetSelectedButton(ApplicationFormBtn);
        }

        private void ProfileBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new Profile(Usernametoprofile.Text);
            SetSelectedButton(ProfileBtn);
        }

        private void PayrollBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new Payroll();
            SetSelectedButton(PayrollBtn);
        }

        private void SupportBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new SupportContacts();
            SetSelectedButton(SupportBtn);
        }

        private void ApprovalBtn_Click(object sender, RoutedEventArgs e)
        {
            ContentDisplay.Content = new UserApproval();
            SetSelectedButton(ApprovalBtn);
        }

        private void SignOutBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult boxResult = MessageBox.Show("Are you sure you want to Sign Out?", "Close Window?", MessageBoxButton.YesNo);
            if (boxResult == MessageBoxResult.Yes)
            {
                Application.Current.MainWindow.Show();
                this.Close();
            }
        }
    }
}
