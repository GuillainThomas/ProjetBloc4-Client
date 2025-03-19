using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using AnnuaireClient.Models;
using AnnuaireClient.Services;

namespace AnnuaireClient;

public partial class MainWindow : Window
{
    private readonly ApiService _apiService = new();
    private int currentPage = 1;
    private int rowsPerPage = 10;
    private List<Employee> displayEmployees = new();

    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Window_Initialized(object sender, EventArgs e)
    {
        displayEmployees = await _apiService.GetEmployeesAsync();
        AgencySelect.ItemsSource = await _apiService.GetAgencyAsync();
        ServiceSelect.ItemsSource = await _apiService.GetServiceAsync();
        ApplyFilters();
    }

    private async void Window_Loaded(object sender, RoutedEventArgs e)
    {
        
    }

    private void DisplayPage(int page)
    {
        int startIndex = (page - 1) * rowsPerPage;
        var pagedData = displayEmployees.Skip(startIndex).Take(rowsPerPage).ToList();

        EmployeeDataGrid.ItemsSource = pagedData;
        PageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayEmployees.Count / rowsPerPage)}";
        EmployeeCountTextBlock.Text = $"{displayEmployees.Count} employés";
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

    private void SearchBox_TextChanged(object sender, TextChangedEventArgs e) => ApplyFilters();
    private void AgencySelect_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
    private void ServiceSelect_SelectionChanged(object sender, SelectionChangedEventArgs e) => ApplyFilters();
    private void ReloadPage_Click(object sender, RoutedEventArgs e) => ApplyFilters();

    private async void ApplyFilters()
    {
        string searchText = SearchBox.Text.ToLower();
        int agencySelected = AgencySelect.SelectedItem as Agency != null ? (AgencySelect.SelectedItem as Agency)!.Id : 0;
        int serviceSelected = ServiceSelect.SelectedItem as Service != null ? (ServiceSelect.SelectedItem as Service)!.Id : 0;

        displayEmployees = await _apiService.GetEmployeesAsync();
        AgencySelect.ItemsSource = await _apiService.GetAgencyAsync();
        ServiceSelect.ItemsSource = await _apiService.GetServiceAsync();

        if (!string.IsNullOrEmpty(searchText))
        {
            displayEmployees = displayEmployees.Where(x => x.LastName.ToLower().Contains(searchText) || x.FirstName.ToLower().Contains(searchText)).ToList();
        }

        if (agencySelected != 0)
        {
            displayEmployees = displayEmployees.Where(x => x.AgencyId == agencySelected).ToList();
        }
        
        if (serviceSelected != 0)
        {
            displayEmployees = displayEmployees.Where(x => x.ServiceId == serviceSelected).ToList();
        }
        
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

    private async void DetailEmployee_Click(object sender, SelectionChangedEventArgs e)
    {
        Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;

        if (selectedEmployee != null)
        {
            DisplayEmployeeWindow detailsWindow = new DisplayEmployeeWindow(selectedEmployee);
            detailsWindow.ShowDialog();
        }
    }

    private void MainWindow_KeyDown(object sender, KeyEventArgs e)
    {
        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)) // Check for Ctrl key pressed
        {
            if (e.Key == Key.F9) // Check for F9 key pressed
            {
                PasswordPromptWindow passwordWindow = new PasswordPromptWindow();

                if (passwordWindow.ShowDialog() == true) // Wait for password input
                {
                    AdminPanelWindow formWindow = new();
                    formWindow.Show();
                }
            }
        }
    }
}