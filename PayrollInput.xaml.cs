using Human_Resources_Management_System.UserControls;
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
using System.Windows.Shapes;

namespace Human_Resources_Management_System
{
    /// <summary>
    /// Interaction logic for PayrollInput.xaml
    /// </summary>
    public partial class PayrollInput : Window
    {
        private const double DailyRate = 300;
        private const double OvertimeRate = 60.70;
        private const double FixedDeduction = 80.21;
        private readonly MongoDbConnection _connection;
        private readonly UserControls.Payroll _payrollUserControl;
        private readonly PeoplesModel _selectedEmployee;

        // Updated Constructor to Accept Payroll User Control
        public PayrollInput(UserControls.Payroll payrollUserControl, PeoplesModel selectedEmployee)
        {
            InitializeComponent();
            _connection = new MongoDbConnection();
            _payrollUserControl = payrollUserControl ?? throw new ArgumentNullException(nameof(payrollUserControl));

            if (selectedEmployee == null)
            {
                MessageBox.Show("No employee selected.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close(); // Close the window if there's no selected employee
                return;
            }

            _selectedEmployee = selectedEmployee;

            // Set the title dynamically based on the selected employee
            Title = $"Payroll Input - {_selectedEmployee.FirstName} {_selectedEmployee.Surname}";
        }

       
       
        private void CalculatePayslip_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get inputs
                int attendanceDays = int.Parse(AttendanceDaysInput.Text);
                double overtimeHours = double.Parse(OvertimeHoursInput.Text);

                // Calculate earnings
                double attendanceEarnings = attendanceDays * DailyRate;
                double overtimeEarnings = overtimeHours * OvertimeRate;
                double totalEarnings = attendanceEarnings + overtimeEarnings - FixedDeduction;

                // Display the result
                ResultBlock.Text = $"Attendance Earnings: {attendanceEarnings:C}\n" +
                                   $"Overtime Earnings: {overtimeEarnings:C}\n" +
                                   $"Total Earnings: {totalEarnings:C}";

                // Create the payslip object
                Payslip payslip = new Payslip
                {
                    EmployeeId = _selectedEmployee.EmployeeId,
                    EmployeeName = $"{_selectedEmployee.FirstName} {_selectedEmployee.Surname}",
                    BasicSalary = (decimal)attendanceEarnings,
                    OvertimePay = (decimal)overtimeEarnings,
                    Deductions = (decimal)FixedDeduction,
                    PayDate = DateTime.Now
                };

                // Update the Payroll User Control
                _payrollUserControl.AddPayslip(payslip);

                
                                                

                MessageBox.Show("Employee Salary Successfully Updated");
                

            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid numeric values for attendance days and overtime hours.",
                                "Input Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}