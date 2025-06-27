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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for SupportContacts.xaml
    /// </summary>
    public partial class SupportContacts : UserControl
    {
        public SupportContacts()
        {
            InitializeComponent();
            Phoneicon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Telephone-Support.PNG"));
            Humanwithphoneicon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Contact-Support-Icon.PNG"));
            Mapicon.Source = new BitmapImage(new Uri("pack://application:,,,/Images/Google-Map-icon.png"));
        }

        private void StackPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender is StackPanel stackPanel && stackPanel.Effect is DropShadowEffect dropShadow)
            {
                // Enlarge the StackPanel
                AnimateStackPanelSize(stackPanel, 300, 350);

                // Fade out the shadow
                AnimateDropShadowOpacity(dropShadow, 0.5, 0);
            }
        }

        private void StackPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender is StackPanel stackPanel && stackPanel.Effect is DropShadowEffect dropShadow)
            {
                // Restore the original size of the StackPanel
                AnimateStackPanelSize(stackPanel, 350, 300);

                // Fade in the shadow
                AnimateDropShadowOpacity(dropShadow, 0, 0.5);
            }
        }

        private void AnimateStackPanelSize(StackPanel stackPanel, double from, double to)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation(from, to, TimeSpan.FromSeconds(0.3));
            DoubleAnimation heightAnimation = new DoubleAnimation(from, to, TimeSpan.FromSeconds(0.3));
            stackPanel.BeginAnimation(WidthProperty, widthAnimation);
            stackPanel.BeginAnimation(HeightProperty, heightAnimation);
        }

        private void AnimateDropShadowOpacity(DropShadowEffect dropShadow, double from, double to)
        {
            DoubleAnimation opacityAnimation = new DoubleAnimation(from, to, TimeSpan.FromSeconds(0.3));
            dropShadow.BeginAnimation(DropShadowEffect.OpacityProperty, opacityAnimation);
        }
    }
}
