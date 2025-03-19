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

            await _apiService.UpdateAgencyAsync(agency_edited);
            this.Close();
        }

        private async void DeleteAgency_Click(object sender, RoutedEventArgs e)
        {
            await _apiService.DeleteAgencyAsync(agency_old.Id);
            this.Close();
        }
    }
}
