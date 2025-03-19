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

            await _apiService.UpdateServiceAsync(service_edited);
            this.Close();
        }

        private async void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            await _apiService.DeleteServiceAsync(service_old.Id);
            this.Close();
        }
    }
}
