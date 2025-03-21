﻿using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;

namespace AnnuaireClient
{
    public partial class CreateAgencyWindow : Window
    {
        private readonly ApiService _apiService = new();

        public Agency newAgency { get; private set; }
        public CreateAgencyWindow()
        {
            InitializeComponent();

            // Nettoyer les champs
            CityBox.Clear();
        }

        private async void CreateAgency_Click(object sender, RoutedEventArgs e)
        {
            // Create the new employee from input
            newAgency = new Agency
            {
                City = CityBox.Text,
            };

            // Envoyer la requête d'ajout
            await _apiService.AddAgencyAsync(newAgency);

            this.DialogResult = true; // Close the form and return success
        }
    }
}
