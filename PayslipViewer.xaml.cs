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
using System.Windows.Shapes;
using Microsoft.Web.WebView2.Core;
using System.IO;


namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for PayslipViewer.xaml
    /// </summary>
    public partial class PayslipViewer : Window
    {
        public PayslipViewer(string pdfPath)
        {
            InitializeComponent();

            Loaded += async (s, e) =>
            {
                await PdfViewer.EnsureCoreWebView2Async();
                PdfViewer.CoreWebView2.Navigate(new Uri(pdfPath).AbsoluteUri);
            };

            this.KeyDown += (s, e) =>
            {
                if (e.Key == Key.Escape)
                {
                    ExitFullScreen();
                }
            };
        }

        private void ExitFullScreen()
        {
            this.WindowState = WindowState.Normal; // Restore the window state
        }

    }
}
