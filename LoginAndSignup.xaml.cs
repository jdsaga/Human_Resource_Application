using Human_Resources_Management_System.UserControls;
using System.Text;
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

namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginAndSignup : Window
    {
        public LoginAndSignup()
        {
            InitializeComponent();
            ContentDisplay.Content = new Login();
            LogoImage.Source = new BitmapImage(new Uri("pack://application:,,,/Images/realcompanylogo.png"));
            Loaded += OnWindowLoaded;

        }

        /* Function para mashow yung login usercontrol at mahide yung signup user control, then vise versa sa function na nasa baba*/
        public void LoginHyperlink() 
        {
            ContentDisplay.Content = new Login();
        }

        public void SignupHyperlink()
        {
            ContentDisplay.Content = new SignUp();

        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {

        }

        public void ForgotPassHyperlink()
        {
            ContentDisplay.Content = new ForgotPass();

        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            // Create animations for LogoImage
            var fadeInLogo = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            LogoImage.BeginAnimation(UIElement.OpacityProperty, fadeInLogo);

            var scaleUpLogoWidth = new DoubleAnimation
            {
                From = 0,
                To = 300,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            var scaleUpLogoHeight = new DoubleAnimation
            {
                From = 0,
                To = 200,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            LogoImage.BeginAnimation(FrameworkElement.WidthProperty, scaleUpLogoWidth);
            LogoImage.BeginAnimation(FrameworkElement.HeightProperty, scaleUpLogoHeight);

            // Create animations for WelcomeText
            var fadeInText = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            WelcomeText.BeginAnimation(UIElement.OpacityProperty, fadeInText);

            var fontSizeText = new DoubleAnimation
            {
                From = 0,
                To = 25,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };
            WelcomeText.BeginAnimation(TextBlock.FontSizeProperty, fontSizeText);
        }


    }
}