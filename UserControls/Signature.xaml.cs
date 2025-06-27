using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Signature.xaml
    /// </summary>
    public partial class Signature : UserControl
    {
        public string SignatureType { get; set; }
        public byte[] _applicantsSignature { get; set; }
        public byte[] _authorizeSignature { get; set; }
        public Signature()
        {
            InitializeComponent();
            // Set up DrawingAttributes for pen pressure sensitivity
            var drawingAttributes = new DrawingAttributes
            {
                Color = System.Windows.Media.Colors.Black,
                Width = 2,
                Height = 2,
                IgnorePressure = false, // Enable pressure sensitivity
                FitToCurve = true // Smooth the strokes
            };

            SignatureCanvas.DefaultDrawingAttributes = drawingAttributes;
        }
    

        // Clear the signature
        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            SignatureCanvas.Strokes.Clear();
        }

        

    private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (SignatureCanvas.Strokes.Count == 0)
            {
                MessageBox.Show("Please provide a signature before confirming.");
                return;
            }

            if (SignatureType == "Applicants")
            {
                _applicantsSignature = CaptureSignature();
                MessageBox.Show("Applicant signature captured.");
            }
            else if (SignatureType == "Authorization")
            {
                _authorizeSignature = CaptureSignature();
                MessageBox.Show("Authorization signature captured.");
            }

            if (_authorizeSignature == null)
            {
                MessageBox.Show("Authorization signature is missing.");
                return; // Prevent insertion if signature is missing
            }

            if (_applicantsSignature == null)
            {
                MessageBox.Show("Applicant signature is missing.");
                return; // Prevent insertion if signature is missing
            }

        }




        private byte[] CaptureSignature()
        {
            double width = SignatureCanvas.ActualWidth;
            double height = SignatureCanvas.ActualHeight;

            // Ensure width and height are valid
            if (width == 0 || height == 0)
            {
                MessageBox.Show("InkCanvas size is invalid.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return null;
            }

            // Create a RenderTargetBitmap
            RenderTargetBitmap renderBitmap = new RenderTargetBitmap(
                (int)width, (int)height,
                96, 96, PixelFormats.Pbgra32);

            // Render the InkCanvas content
            DrawingVisual drawingVisual = new DrawingVisual();
            using (DrawingContext context = drawingVisual.RenderOpen())
            {
                VisualBrush brush = new VisualBrush(SignatureCanvas);
                context.DrawRectangle(brush, null, new Rect(new Point(), new Size(width, height)));
            }

            renderBitmap.Render(drawingVisual);

            // Encode the bitmap to a PNG byte array
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(renderBitmap));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                return memoryStream.ToArray(); // Return the byte array representing the signature
            }
        }
    }
}
