using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DetailServiceWindow : Window
    {
        private readonly ApiService _apiService = new();
        private Service service_old;

        public DetailServiceWindow(Service service)
        {
            InitializeComponent();

            // Initialiser les champs
            if (service != null)
            {
                NameBox.Text = service.Name;
                service_old = service;
            }
        }

        private async void EditService_Click(object sender, RoutedEventArgs e)
        {
            Service service_edited = new()
            {
                Id = service_old.Id,
                Name = NameBox.Text,
            };

            // Envoyer la requête de modification
            await _apiService.UpdateServiceAsync(service_edited);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }

        private async void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            // Envoyer la requête de suppression
            await _apiService.DeleteServiceAsync(service_old.Id);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }
    }
}
