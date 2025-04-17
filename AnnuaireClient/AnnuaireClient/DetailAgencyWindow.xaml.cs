using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class DetailAgencyWindow : Window
    {
        private readonly ApiService _apiService = new();
        private Agency agency_old;

        public DetailAgencyWindow(Agency agency)
        {
            InitializeComponent();

            // Initialiser les champs
            if (agency != null)
            {
                CityBox.Text = agency.City;
                agency_old = agency;
            }
        }

        private async void EditAgency_Click(object sender, RoutedEventArgs e)
        {
            Agency agency_edited = new()
            {
                Id = agency_old.Id,
                City = CityBox.Text,
            };

            // Envoyer la requête de modification
            await _apiService.UpdateAgencyAsync(agency_edited);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }

        private async void DeleteAgency_Click(object sender, RoutedEventArgs e)
        {
            // Envoyer la requête de suppression
            await _apiService.DeleteAgencyAsync(agency_old.Id);

            // Fermer la fenêtre et retourner un succès
            this.Close();
        }
    }
}
