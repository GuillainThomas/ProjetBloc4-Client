using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DetailEmployeeWindow : Window
    {
        private readonly ApiService _apiService = new();

        private Employee employee_old;
        public DetailEmployeeWindow(Employee employee)
        {
            InitializeComponent();

            if (employee != null)
            {
                employee_old = employee;
                LastNameBox.Text = employee.LastName;
                FirstNameBox.Text = employee.FirstName;
                MobilePhoneNumberBox.Text = employee.MobilePhoneNumber;
                FixedPhoneNumberBox.Text = employee.FixedPhoneNumber;
                EmailBox.Text = employee.Email;
                AgencySelect.SelectedItem = employee.Agency;
                ServiceSelect.SelectedItem = employee.Service;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AgencySelect.ItemsSource = await _apiService.GetAgencyAsync();
            ServiceSelect.ItemsSource = await _apiService.GetServiceAsync();
        }

        private async void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee_edited = new()
            {
                Id = employee_old.Id,
                LastName = LastNameBox.Text,
                FirstName = FirstNameBox.Text,
                MobilePhoneNumber = MobilePhoneNumberBox.Text,
                FixedPhoneNumber = FixedPhoneNumberBox.Text,
                Email = EmailBox.Text,
                AgencyId = (AgencySelect.SelectedItem as Agency)!.Id,
                ServiceId = (ServiceSelect.SelectedItem as Service)!.Id,
            };

            await _apiService.UpdateEmployeeAsync(employee_edited);
            this.Close();
        }

        private async void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            await _apiService.DeleteEmployeeAsync(employee_old.Id);
            this.Close();
        }
    }
}
