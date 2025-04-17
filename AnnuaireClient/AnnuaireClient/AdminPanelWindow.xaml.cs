using AnnuaireClient.Models;
using AnnuaireClient.Services;
using AnnuaireClient.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireClient
{
    public partial class AdminPanelWindow : Window
    {
        private readonly ApiService _apiService = new();

        private List<Agency> displayAgencies = new();
        private List<Employee> displayEmployees = new();
        private List<Service> displayServices = new();

        private int currentPage = 1;
        private int rowsPerPage = 10;
        private int previousTabSelected = 0;

        private ObservableCollection<EmployeeViewModel> Employees { get; set; }

        public AdminPanelWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayEmployees = await _apiService.GetAllEmployeeAsync();
            displayAgencies = await _apiService.GetAllAgencyAsync();
            displayServices = await _apiService.GetAllServiceAsync();

            DisplayPage();
        }

        // Change le contenu de la page affichée suivant l'onglet sélectionné
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (previousTabSelected != TabControl.SelectedIndex)
            {
                previousTabSelected = TabControl.SelectedIndex;
                currentPage = 1;
                DisplayPage();
            }
        }

        private async void DisplayPage()
        {
            int startIndex = (currentPage - 1) * rowsPerPage;

            // Charge les données de la page sélectionnée suivant l'onglet sélectionné
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    Employees = new((await _apiService.GetAllEmployeeAsync()).Skip(startIndex).Take(rowsPerPage).ToList()
                        .Select(e => new EmployeeViewModel(e, displayAgencies.FirstOrDefault(a => a.Id == e.AgencyId), displayServices.FirstOrDefault(s => s.Id == e.ServiceId)))
                    );
                    EmployeeDataGrid.ItemsSource = Employees;
                    EmployeePageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayEmployees.Count / rowsPerPage)}";
                    break;
                case 1:
                    var agencyPagedData = (await _apiService.GetAllAgencyAsync()).Skip(startIndex).Take(rowsPerPage).ToList();
                    AgencyDataGrid.ItemsSource = agencyPagedData;
                    AgencyPageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayAgencies.Count / rowsPerPage)}";
                    break;
                case 2:
                    var servicePagedData = (await _apiService.GetAllServiceAsync()).Skip(startIndex).Take(rowsPerPage).ToList();
                    ServiceDataGrid.ItemsSource = servicePagedData;
                    ServicePageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayServices.Count / rowsPerPage)}";
                    break;
                default:
                    break;
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            if (currentPage > 1)
            {
                currentPage--;
                DisplayPage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            int maxPage = 0;

            // Calcule le nombre de pages max suivant l'onglet sélectionné
            switch (TabControl.SelectedIndex)
            {
                case 0:
                    maxPage = (int)Math.Ceiling((double)displayEmployees.Count / rowsPerPage);
                    break;
                case 1:
                    maxPage = (int)Math.Ceiling((double)displayAgencies.Count / rowsPerPage);
                    break;
                case 2:
                    maxPage = (int)Math.Ceiling((double)displayServices.Count / rowsPerPage);
                    break;
                default:
                    break;
            }

            if (currentPage < maxPage)
            {
                currentPage++;
                DisplayPage();
            }
        }

        private void OpenCreateEmployee_Click(object sender, RoutedEventArgs e)
        {
            CreateEmployeeWindow createWindow = new();

            if (createWindow.ShowDialog() == true)
                DisplayPage();
        }

        private void OpenCreateAgency_Click(object sender, RoutedEventArgs e)
        {
            CreateAgencyWindow createWindow = new();

            if (createWindow.ShowDialog() == true)
                DisplayPage();
        }

        private void OpenCreateService_Click(object sender, RoutedEventArgs e)
        {
            CreateServiceWindow createWindow = new();

            if (createWindow.ShowDialog() == true)
                DisplayPage();
        }

        private void DetailEmployee_Click(object sender, RoutedEventArgs e)
        {
            EmployeeViewModel selectedEmployee = (EmployeeViewModel)EmployeeDataGrid.SelectedItem;
            if (selectedEmployee != null)
            {
                DetailEmployeeWindow detailWindow = new(selectedEmployee);
                detailWindow.Show();
                DisplayPage();
            }
        }

        private void DetailAgency_Click(object sender, RoutedEventArgs e)
        {
            Agency selectedAgency = (Agency)AgencyDataGrid.SelectedItem;
            if (selectedAgency != null)
            {
                DetailAgencyWindow detailWindow = new(selectedAgency);
                detailWindow.Show();
                DisplayPage();
            }
        }

        private void DetailService_Click(object sender, RoutedEventArgs e)
        {
            Service selectedService = (Service)ServiceDataGrid.SelectedItem;
            if (selectedService != null)
            {
                DetailServiceWindow detailWindow = new(selectedService);
                detailWindow.Show();
                DisplayPage();
            }
        }
    }
}
