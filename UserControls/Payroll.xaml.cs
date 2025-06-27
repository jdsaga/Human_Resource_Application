using SharpVectors.Dom;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using SharpVectors.Dom;
using System.IO;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Microsoft.Web.WebView2.Core;
using MongoDB.Driver;
using System.Data.Common;

namespace Human_Resources_Management_System.UserControls
{
    /// <summary>
    /// Interaction logic for Payroll.xaml
    /// </summary>
    
    public partial class Payroll : UserControl
    {

        private string _recentPayslipPath;
        private readonly MongoDbConnection _connection;
        public ObservableCollection<Payslip> Payslips { get; set; } = new ObservableCollection<Payslip>();
        public Payroll()
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            LoadData();
            DataContext = this; // Ensures bindings are recognized
          


        }

        public void LoadData()
        {
            try
            {
                var peoplesCollection = _connection.GetPeoplesCollection();
                var users = peoplesCollection.Find(FilterDefinition<PeoplesModel>.Empty).ToList();

                if (users == null || users.Count == 0)
                {
                    MessageBox.Show("No records found in the database.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Bind the ListView directly to the database data
                ListViewUsers.ItemsSource = users;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void AddPayslip(Payslip payslip)
        {
            if (payslip != null)
            {
                // Store the latest calculated payslip
              
                // Update or add the payslip in the ObservableCollection
                var existingPayslip = Payslips.FirstOrDefault(p => p.EmployeeId == payslip.EmployeeId);

                if (existingPayslip != null)
                {
                    // Update existing payslip
                    existingPayslip.BasicSalary = payslip.BasicSalary;
                    existingPayslip.OvertimePay = payslip.OvertimePay;
                    existingPayslip.Deductions = payslip.Deductions;
                    existingPayslip.PayDate = payslip.PayDate;
                }
                else
                {
                    // Add a new payslip
                    Payslips.Add(payslip);
                }

                // Update Total Employee Count and Total Payroll
                Total_Employee1.Text = Payslips.Count.ToString();
                Total_Payroll1.Text = $"{Payslips.Sum(p => p.NetPay):C}";

                // Refresh ListView to show the updated data
                ListViewUsers.Items.Refresh();
            }
        }

        private void GeneratePayslipButton_Click(object sender, RoutedEventArgs e)
        {

            var selectedEmployee = ListViewUsers.SelectedItem as PeoplesModel;

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee from the list.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Find the payslip for the selected employee
            var payslip = Payslips.FirstOrDefault(p => p.EmployeeId == selectedEmployee.EmployeeId);

            if (payslip == null)
            {
                MessageBox.Show("No payslip available for the selected employee.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }

            // Update the database
            var peoplesCollection = _connection.GetPeoplesCollection();
            var filter = Builders<PeoplesModel>.Filter.Eq(e => e.EmployeeId, payslip.EmployeeId);
            var update = Builders<PeoplesModel>.Update
                .Set(e => e.Pay, payslip.NetPay)
                .Set(e => e.DatePaid, DateTime.Now);
            peoplesCollection.UpdateOne(filter, update);

            // Reload the ListView data in Payroll
            LoadData(); // Call LoadData to refresh the ListView

            // Display payslip details in a MessageBox or new window
            string payslipDetails = $"Employee Name: {payslip.EmployeeName}\n" +
                                    $"Employee ID: {payslip.EmployeeId}\n" +
                                    $"Basic Salary: {payslip.BasicSalary:C}\n" +
                                    $"Overtime Pay: {payslip.OvertimePay:C}\n" +
                                    $"Deductions: {payslip.Deductions:C}\n" +
                                    $"Net Pay: {payslip.NetPay:C}\n" +
                                    $"Pay Date: {payslip.PayDate:d}";

            MessageBox.Show(payslipDetails, "Generated Payslip", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExportPayslipButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedEmployee = ListViewUsers.SelectedItem as PeoplesModel;

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee to export the payslip.");
                return;
            }

            // Find the corresponding payslip
            var payslip = Payslips.FirstOrDefault(p => p.EmployeeId == selectedEmployee.EmployeeId);

            if (payslip == null)
            {
                MessageBox.Show("No payslip available for the selected employee.");
                return;
            }

            // Generate the file name for the payslip PDF
            string fileName = $"{selectedEmployee.FirstName}{selectedEmployee.Surname}Payslip{DateTime.Now.ToString("yyyyMMdd_HHmmss")}.pdf";

            // Get the path to the user's "Documents" folder and combine it with the "Payslips" folder
            string userDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string payslipsFolderPath = System.IO.Path.Combine(userDocumentsPath, "Payslips");

            // Ensure the "Payslips" directory exists
            if (!Directory.Exists(payslipsFolderPath))
            {
                Directory.CreateDirectory(payslipsFolderPath);
            }

            // Combine the path with the file name
            _recentPayslipPath = System.IO.Path.Combine(payslipsFolderPath, fileName);

            // Generate the PDF and save it
            GeneratePayslipPDF(payslip, _recentPayslipPath);

            // Show a message to the user
            MessageBox.Show($"Payslip exported to {_recentPayslipPath}");
        }


        private void GeneratePayslipPDF(Payslip payslip, string filePath)
        {
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Salary Slip";

            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);

            // Fonts
            XFont titleFont = new XFont("Arial", 24);
            XFont headerFont = new XFont("Arial", 12);
            XFont contentFont = new XFont("Arial", 10);

            // Title
            gfx.DrawString("SALARY SLIP", titleFont, XBrushes.Black, new XRect(0, 20, page.Width, 40), XStringFormats.Center);

            // Employee Details
            gfx.DrawString($"Name: {payslip.EmployeeName}", contentFont, XBrushes.Black, new XPoint(40, 60));
            gfx.DrawString($"Employee ID: {payslip.EmployeeId:D5}", contentFont, XBrushes.Black, new XPoint(400, 60));
            gfx.DrawString($"Pay Date: {payslip.PayDate:MMMM dd, yyyy}", contentFont, XBrushes.Black, new XPoint(40, 80));

            // Table Header for First Table
            double xStart = 40;
            double yStart = 120;
            double rowHeight = 25;
            double col1Width = 200;
            double col2Width = 100;
            double col3Width = 100;

            // Draw line above header
            double tableWidth = col1Width + col2Width + col3Width;
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Draw Header
            gfx.DrawString("Description", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Earnings", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Deductions", headerFont, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Draw horizontal line under header
            gfx.DrawLine(XPens.Black, xStart, yStart + rowHeight, xStart + tableWidth, yStart + rowHeight);

            yStart += rowHeight;

            // First Table Content
            DrawTableRow(gfx, "Daily Rate", "300", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Hourly Rate", "24.375", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Monthly Rate", "8,775.00", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Overtime", "60.70", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Tax", "", "80.21", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Total", "8,755.49", "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);

            /*
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart); // Line above total row
            gfx.DrawString("Total", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width + col2Width, rowHeight, true), XStringFormats.Center); // Total label
            gfx.DrawString("8,755.49", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width + col3Width, rowHeight, true), XStringFormats.CenterLeft); */

            // Draw Vertical Lines
            gfx.DrawLine(XPens.Black, xStart, 120, xStart, yStart); // First vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width, 120, xStart + col1Width, yStart); // Second vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width + col2Width, 120, xStart + col1Width + col2Width, yStart); // Third vertical line
            gfx.DrawLine(XPens.Black, xStart + tableWidth, 120, xStart + tableWidth, yStart); // Last vertical line

            // Draw Final Horizontal Line Below Total Row
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Add spacing before Second Table
            yStart += 40;

            // Second Table Header
            double secondTableStart = yStart;
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart); // Horizontal line above the header row
            gfx.DrawString("Description", headerFont, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Earnings", headerFont, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString("Deductions", headerFont, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Draw horizontal line under header
            gfx.DrawLine(XPens.Black, xStart, yStart + rowHeight, xStart + tableWidth, yStart + rowHeight);

            yStart += rowHeight;

            // Second Table Content (Dynamic)
            DrawTableRow(gfx, "Basic Salary", payslip.BasicSalary.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Overtime Pay", payslip.OvertimePay.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Deductions", "", payslip.Deductions.ToString("C"), xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);
            DrawTableRow(gfx, "Net Pay", payslip.NetPay.ToString("C"), "", xStart, ref yStart, col1Width, col2Width, col3Width, contentFont, rowHeight, true);


            // Draw vertical lines for Second Table (adjusted length)
            gfx.DrawLine(XPens.Black, xStart, secondTableStart, xStart, yStart); // First vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width, secondTableStart, xStart + col1Width, yStart); // Second vertical line
            gfx.DrawLine(XPens.Black, xStart + col1Width + col2Width, secondTableStart, xStart + col1Width + col2Width, yStart); // Third vertical line
            gfx.DrawLine(XPens.Black, xStart + tableWidth, secondTableStart, xStart + tableWidth, yStart); // Last vertical line

            // Draw final horizontal line at the bottom of the second table
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + tableWidth, yStart);

            // Save PDF
            document.Save(filePath);
        }



        // Helper method to draw a table row
        private void DrawTableRow(XGraphics gfx, string description, string earnings, string deductions, double xStart, ref double yStart, double col1Width, double col2Width, double col3Width, XFont font, double rowHeight, bool centerDescription)
        {
            // Draw row content
            if (centerDescription)
            {
                gfx.DrawString(description, font, XBrushes.Black, new XRect(xStart, yStart, col1Width, rowHeight), XStringFormats.Center);
            }
            else
            {
                gfx.DrawString(description, font, XBrushes.Black, new XPoint(xStart + 10, yStart + 15)); // Default left-aligned
            }

            gfx.DrawString(earnings, font, XBrushes.Black, new XRect(xStart + col1Width, yStart, col2Width, rowHeight), XStringFormats.Center);
            gfx.DrawString(deductions, font, XBrushes.Black, new XRect(xStart + col1Width + col2Width, yStart, col3Width, rowHeight), XStringFormats.Center);

            // Move to the next row
            yStart += rowHeight;

            // Draw horizontal line for the row
            gfx.DrawLine(XPens.Black, xStart, yStart, xStart + col1Width + col2Width + col3Width, yStart);
        }

        private void ViewPayslipButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Check if the path is null or empty
                if (string.IsNullOrEmpty(_recentPayslipPath))
                {
                    MessageBox.Show("Error: Payslip path is not set.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check if the file exists
                if (!File.Exists(_recentPayslipPath))
                {
                    MessageBox.Show("No recent payslip found or the file was deleted.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Open the recent payslip
                PayslipViewer viewer = new PayslipViewer(_recentPayslipPath);
                viewer.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message, "Critical Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenPayrollInput_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the ListView
            var selectedEmployee = ListViewUsers.SelectedItem as PeoplesModel;

            if (selectedEmployee == null)
            {
                MessageBox.Show("Please select an employee from the list.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
           
            // Debugging: Confirm the selected employee's name
            MessageBox.Show($"Selected employee: {selectedEmployee.FirstName} {selectedEmployee.Surname}", "Selected Employee");

            // Open PayrollInput with selected employee data
            var payrollInputWindow = new PayrollInput(this, selectedEmployee); // Pass the current Payroll instance
            payrollInputWindow.ShowDialog();
        }

        private void ToggleSearchBar(object sender, RoutedEventArgs e)
        {
            if (SearchBar.Visibility == Visibility.Collapsed)
            {
                SearchBar.Visibility = Visibility.Visible;
                SearchBar.Focus();
            }
            else
            {
                SearchBar.Visibility = Visibility.Collapsed;
                SearchBar.Text = string.Empty; // Clear search when hiding

                // Reset the ListView to show all items
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewUsers.ItemsSource);
                view.Filter = null; // Remove any filters
            }
        }
        private void SearchBar_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            string searchText = SearchBar.Text.ToLower();

            // Apply filtering to the ListView based on FirstName
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(ListViewUsers.ItemsSource);
            if (!string.IsNullOrEmpty(searchText))
            {
                view.Filter = item =>
                {
                    if (item is PeoplesModel person)
                    {
                        return person.FirstName.ToLower().Contains(searchText);
                    }
                    return false;
                };
            }
            else
            {
                view.Filter = null; // Reset the filter when the search box is empty
            }
        }
    }
    public class Item
    {
        public string Name { get; set; }
    }


    public class Payslip
    {
        public string EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public decimal BasicSalary { get; set; }
        public decimal OvertimePay { get; set; }
        public decimal Deductions { get; set; }
        public decimal NetPay => BasicSalary + OvertimePay - Deductions;
        public DateTime PayDate { get; set; }
    }
}
