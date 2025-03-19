using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class CreateServiceWindow : Window
    {
        private readonly ApiService _apiService = new();

        public Service newService { get; private set; }
        public CreateServiceWindow()
        {
            InitializeComponent();

            // Nettoyer les champs
            NameBox.Clear();
        }

        private async void CreateService_Click(object sender, RoutedEventArgs e)
        {
            // Create the new employee from input
            newService = new Service
            {
                Name = NameBox.Text,
            };

            // Envoyer la requête d'ajout
            await _apiService.AddServiceAsync(newService);

            this.DialogResult = true; // Close the form and return success
        }
    }
}
