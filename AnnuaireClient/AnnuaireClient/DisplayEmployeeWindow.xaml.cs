using AnnuaireClient.ViewModels;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DisplayEmployeeWindow : Window
    {
        public DisplayEmployeeWindow(EmployeeViewModel employeeData)
        {
            InitializeComponent();

            // Initialiser les champs
            if (employeeData != null)
            {
                EmployeeLastnameTextBox.Text = employeeData.LastName;
                EmployeeFirstnameTextBox.Text = employeeData.FirstName;
                EmployeePhoneNumberTextBox.Text = employeeData.MobilePhoneNumber;
                EmployeeFixedPhoneNumberTextBox.Text = employeeData.FixedPhoneNumber;
                EmployeeEmailTextBox.Text = employeeData.Email;
                EmployeeAgencyTextBox.Text = employeeData.AgencyCity;
                EmployeeServiceTextBox.Text = employeeData.ServiceName;
            }
        }
    }
}
