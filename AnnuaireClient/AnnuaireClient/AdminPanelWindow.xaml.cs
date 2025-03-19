using AnnuaireClient.Models;
using AnnuaireClient.Services;
using System.Windows;
using System.Windows.Controls;

namespace AnnuaireClient
{
    public partial class AdminPanelWindow : Window
    {
        private readonly ApiService _apiService = new();
        private int currentPage = 1;
        private int rowsPerPage = 10;
        private int previousTabSelected = 0;
        private List<Employee> displayEmployees = new();
        private List<Agency> displayAgencies = new();
        private List<Service> displayServices = new();

        public AdminPanelWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayEmployees = await _apiService.GetEmployeesAsync();
            displayAgencies = await _apiService.GetAgencyAsync();
            displayServices = await _apiService.GetServiceAsync();

            DisplayPage();
        }

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

            switch (TabControl.SelectedIndex)
            {
                case 0:
                    //var employeePagedData = displayEmployees.Skip(startIndex).Take(rowsPerPage).ToList();
                    var employeePagedData = (await _apiService.GetEmployeesAsync()).Skip(startIndex).Take(rowsPerPage).ToList();
                    EmployeeDataGrid.ItemsSource = employeePagedData;
                    EmployeePageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayEmployees.Count / rowsPerPage)}";
                    break;
                case 1:
                    //var agencyPagedData = displayAgencies.Skip(startIndex).Take(rowsPerPage).ToList();
                    var agencyPagedData = (await _apiService.GetAgencyAsync()).Skip(startIndex).Take(rowsPerPage).ToList();
                    AgencyDataGrid.ItemsSource = agencyPagedData;
                    AgencyPageNumberText.Text = $"Page {currentPage}/{(int)Math.Ceiling((double)displayAgencies.Count / rowsPerPage)}";
                    break;
                case 2:
                    //var servicePagedData = displayServices.Skip(startIndex).Take(rowsPerPage).ToList();
                    var servicePagedData = (await _apiService.GetServiceAsync()).Skip(startIndex).Take(rowsPerPage).ToList();
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
            {
                DisplayPage();
            }
        }
        private void OpenCreateAgency_Click(object sender, RoutedEventArgs e)
        {
            CreateAgencyWindow createWindow = new();

            if (createWindow.ShowDialog() == true)
            {
                DisplayPage();
            }
        }
        private void OpenCreateService_Click(object sender, RoutedEventArgs e)
        {
            CreateServiceWindow createWindow = new();

            if (createWindow.ShowDialog() == true)
            {
                DisplayPage();
            }
        }

        private void DetailEmployee_Click(object sender, RoutedEventArgs e)
        {
            Employee selectedEmployee = EmployeeDataGrid.SelectedItem as Employee;
            if (selectedEmployee != null)
            {
                DetailEmployeeWindow detailWindow = new DetailEmployeeWindow(selectedEmployee);
                detailWindow.Show();
                DisplayPage();
            }
        }
        private void DetailAgency_Click(object sender, RoutedEventArgs e)
        {
            Agency selectedAgency = AgencyDataGrid.SelectedItem as Agency;
            if (selectedAgency != null)
            {
                DetailAgencyWindow detailWindow = new DetailAgencyWindow(selectedAgency);
                detailWindow.Show();
                DisplayPage();
            }
        }
        private void DetailService_Click(object sender, RoutedEventArgs e)
        {
            Service selectedService = ServiceDataGrid.SelectedItem as Service;
            if (selectedService != null)
            {
                DetailServiceWindow detailWindow = new DetailServiceWindow(selectedService);
                detailWindow.Show();
                DisplayPage();
            }
        }
    }
}
