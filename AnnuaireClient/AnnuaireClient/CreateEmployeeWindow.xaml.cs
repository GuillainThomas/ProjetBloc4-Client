using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class CreateEmployeeWindow : Window
    {
        private readonly ApiService _apiService = new();

        public Employee newEmployee { get; private set; }
        public CreateEmployeeWindow()
        {
            InitializeComponent();

            // Nettoyer les champs
            LastNameBox.Clear();
            FirstNameBox.Clear();
            MobilePhoneNumberBox.Clear();
            FixedPhoneNumberBox.Clear();
            EmailBox.Clear();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AgencySelect.ItemsSource = await _apiService.GetAgencyAsync();
            ServiceSelect.ItemsSource = await _apiService.GetServiceAsync();
        }

        private async void CreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            // Create the new employee from input
            newEmployee = new Employee
            {
                LastName = LastNameBox.Text,
                FirstName = FirstNameBox.Text,
                MobilePhoneNumber = MobilePhoneNumberBox.Text,
                FixedPhoneNumber = FixedPhoneNumberBox.Text,
                Email = EmailBox.Text,
                AgencyId = (AgencySelect.SelectedItem as Agency)!.Id,
                ServiceId = (ServiceSelect.SelectedItem as Service)!.Id,
            };

            // Envoyer la requête d'ajout
            await _apiService.AddEmployeeAsync(newEmployee);

            this.DialogResult = true; // Close the form and return success
        }
    }
}
