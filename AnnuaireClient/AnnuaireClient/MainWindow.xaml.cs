using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnnuaireClient.Models;
using AnnuaireClient.Services;
using AnnuaireClient.ViewModels;

namespace AnnuaireClient;

public partial class MainWindow : Window
{
    private readonly ApiService _apiService = new();

    private List<Employee> displayEmployees = new();
    private List<Agency> listAgence = new();
    private List<Service> listService = new();

    private int currentPage = 1;
    private int rowsPerPage = 10;

    private ObservableCollection<EmployeeViewModel> Employees { get; set; }

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Window_Initialized(object sender, EventArgs e)
    {
        displayEmployees = await _apiService.GetAllEmployeeAsync();
        AgencySelect.ItemsSource = await _apiService.GetAllAgencyAsync();
        ServiceSelect.ItemsSource = await _apiService.GetAllServiceAsync();
        listAgence = await _apiService.GetAllAgencyAsync();
        listService = await _apiService.GetAllServiceAsync();
        ApplyFilters();
    }

    private void DisplayPage(int page)
    {
        int startIndex = (page - 1) * rowsPerPage;
        var pagedData = Employees.Skip(startIndex).Take(rowsPerPage).ToList();

        EmployeeDataGrid.ItemsSource = pagedData;
        PageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)Employees.Count / rowsPerPage)}";
        EmployeeCountTextBlock.Text = $"{Employees.Count} employés";
    }

    private void PreviousPage_Click(object sender, RoutedEventArgs e)
    {
        if (currentPage > 1)
        {
            currentPage--;
            DisplayPage(currentPage);
        }
    }
    private void NextPage_Click(object sender, RoutedEventArgs e)
    {
        int maxPage = (int)Math.Ceiling((double)displayEmployees.Count / rowsPerPage);
        if (currentPage < maxPage)
        {
            currentPage++;
            DisplayPage(currentPage);
        }
    }

    // Appelle les méthodes de filtrage lorsque le texte de recherche ou la sélection d'agence ou de service change
    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
    private void AgencySelect_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
    private void ServiceSelect_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();

    // Recharge la liste des agences et services
    private async void ReloadPage_Click(object sender, RoutedEventArgs e)
    {
        AgencySelect.ItemsSource = await _apiService.GetAllAgencyAsync();
        ServiceSelect.ItemsSource = await _apiService.GetAllServiceAsync();
        listAgence = await _apiService.GetAllAgencyAsync();
        listService = await _apiService.GetAllServiceAsync();
        ApplyFilters();
    }

    // Applique les filtres de recherche sur la liste des employés
    private async void ApplyFilters()
    {
        string searchText = SearchBox.Text.ToLower();
        int agencySelected = AgencySelect.SelectedItem as Agency != null ? (AgencySelect.SelectedItem as Agency)!.Id : 0;
        int serviceSelected = ServiceSelect.SelectedItem as Service != null ? (ServiceSelect.SelectedItem as Service)!.Id : 0;

        displayEmployees = await _apiService.GetFilteredEmployeeAsync(agencySelected == 0 ? string.Empty : agencySelected.ToString(), serviceSelected == 0 ? string.Empty : serviceSelected.ToString(), searchText);

        Employees = new ObservableCollection<EmployeeViewModel>(
                displayEmployees.Select(e => new EmployeeViewModel(
                    e,
                    listAgence.FirstOrDefault(a => a.Id == e.AgencyId),
                    listService.FirstOrDefault(s => s.Id == e.ServiceId)
                ))
            );

        currentPage = 1;
        DisplayPage(currentPage);
    }

    private void ClearFilter_Click(object sender, RoutedEventArgs e)
    {
        SearchBox.Text = "";
        AgencySelect.SelectedItem = null;
        ServiceSelect.SelectedItem = null;
        ApplyFilters();
    }

    // Ouvre la fenêtre de détails de l'employé sélectionné
    private void DetailEmployee_Click(object sender, SelectionChangedEventArgs e)
    {
        EmployeeViewModel selectedEmployee = EmployeeDataGrid.SelectedItem as EmployeeViewModel;

        if (selectedEmployee != null)
        {
            DisplayEmployeeWindow detailsWindow = new(selectedEmployee);
            detailsWindow.ShowDialog();
        }
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
        // Vérifier si la touche Ctrl est pressée
        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
        {
            // Verifier si la touche F9 est pressée
            if (e.Key == Key.F9)
            {
                PasswordPromptWindow passwordWindow = new();

                //Ouvre la fenêtre de mot de passe et attend la réponse
                if (passwordWindow.ShowDialog() == true)
                {
                    AdminPanelWindow formWindow = new();
                    formWindow.Show();
                }
            }
        }
    }
}