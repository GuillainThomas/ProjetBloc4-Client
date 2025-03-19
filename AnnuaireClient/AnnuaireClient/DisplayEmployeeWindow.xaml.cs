using AnnuaireClient.Models;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DisplayEmployeeWindow : Window
    {
        public DisplayEmployeeWindow(Employee employee)
        {
            InitializeComponent();

            if (employee != null)
            {
                EmployeeLastnameTextBox.Text = employee.LastName;
                EmployeeFirstnameTextBox.Text = employee.FirstName;
                EmployeePhoneNumberTextBox.Text = employee.MobilePhoneNumber;
                EmployeeFixedPhoneNumberTextBox.Text = employee.FixedPhoneNumber;
                EmployeeEmailTextBox.Text = employee.Email;
                EmployeeAgencyTextBox.Text = employee.Agency.City;
                EmployeeServiceTextBox.Text = employee.Service.Name;
            }
        }
    }
}
