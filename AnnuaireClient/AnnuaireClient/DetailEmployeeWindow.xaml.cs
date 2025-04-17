using AnnuaireClient.Models;
using AnnuaireClient.Services;
using AnnuaireClient.ViewModels;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DetailEmployeeWindow : Window
    {
        private readonly ApiService _apiService = new();

        private EmployeeViewModel Employee { get; set; }
        private EmployeeViewModel Employee_old { get; set; }

        public DetailEmployeeWindow(EmployeeViewModel employeeData)
        {
            InitializeComponent();

            // Initialiser les champs
            if (employeeData != null)
            {
                Employee = employeeData;
                Employee_old = Employee;
                LastNameBox.Text = employeeData.LastName;
                FirstNameBox.Text = employeeData.FirstName;
                MobilePhoneNumberBox.Text = employeeData.MobilePhoneNumber;
                FixedPhoneNumberBox.Text = employeeData.FixedPhoneNumber;
                EmailBox.Text = employeeData.Email;
                AgencySelect.SelectedIndex = Employee.AgencyId;
                ServiceSelect.SelectedIndex = Employee.ServiceId;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Remplir les listes déroulantes avec les agences et services
            AgencySelect.ItemsSource = await _apiService.GetAllAgencyAsync();
            ServiceSelect.ItemsSource = await _apiService.GetAllServiceAsync();
        }

        private async void EditEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee employee_edited = new()
            {
                Id = Employee_old.EmployeeId,
                LastName = LastNameBox.Text,
                FirstName = FirstNameBox.Text,
                MobilePhoneNumber = MobilePhoneNumberBox.Text,
                FixedPhoneNumber = FixedPhoneNumberBox.Text,
                Email = EmailBox.Text,
                AgencyId = (AgencySelect.SelectedItem as Agency)!.Id,
                ServiceId = (ServiceSelect.SelectedItem as Service)!.Id,
            };

            // Envoyer la requête de modification
            await _apiService.UpdateEmployeeAsync(employee_edited);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }

        private async void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Envoyer la requête de suppression
            await _apiService.DeleteEmployeeAsync(Employee_old.EmployeeId);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }
    }
}
