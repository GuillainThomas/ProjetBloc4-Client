using AnnuaireClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace AnnuaireClient.Services;

public class ApiService
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7278/api/") }; // API Debug
    //private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:5003/api/") }; // API Release

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Employee>>("Employee");
    }
    public async Task<List<Agency>> GetAgencyAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Agency>>("Agency");
    }
    public async Task<List<Service>> GetServiceAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Service>>("Service");
    }

    public async Task AddEmployeeAsync(Employee employee)
    {
        await _httpClient.PostAsJsonAsync("Employee", employee);
    }
    public async Task AddAgencyAsync(Agency agency)
    {
        await _httpClient.PostAsJsonAsync("Agency", agency);
    }
    public async Task AddServiceAsync(Service service)
    {
        await _httpClient.PostAsJsonAsync("Service", service);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _httpClient.PutAsJsonAsync($"Employee/{employee.Id}" ,employee);
    }
    public async Task UpdateAgencyAsync(Agency agency)
    {
        await _httpClient.PutAsJsonAsync($"Agency/{agency.Id}", agency);
    }
    public async Task UpdateServiceAsync(Service service)
    {
        await _httpClient.PutAsJsonAsync($"Service/{service.Id}", service);
    }

    public async Task DeleteEmployeeAsync(int id)
    {
        await _httpClient.DeleteAsync($"Employee/{id}");
    }
    public async Task DeleteAgencyAsync(int id)
    {
        await _httpClient.DeleteAsync($"Agency/{id}");
    }
    public async Task DeleteServiceAsync(int id)
    {
        await _httpClient.DeleteAsync($"Service/{id}");
    }
}
