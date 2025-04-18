using AnnuaireClient.Models;
using System.Net.Http;
using System.Net.Http.Json;

namespace AnnuaireClient.Services;

public class ApiService
{
    private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new Uri("https://localhost:7278/api/") };

    // CRUD pour les employées
    public async Task<List<Employee>> GetAllEmployeeAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Employee>>("Employee");
    }
    public async Task<List<Employee>> GetFilteredEmployeeAsync(string? agencyId, string? serviceId, string? name)
    {
        return await _httpClient.GetFromJsonAsync<List<Employee>>($"Employee/filter?agencyId={agencyId}&serviceId={serviceId}&name={name}");
    }
    public async Task AddEmployeeAsync(Employee employee)
    {
        await _httpClient.PostAsJsonAsync("Employee", employee);
    }
    public async Task UpdateEmployeeAsync(Employee employee)
    {
        await _httpClient.PutAsJsonAsync($"Employee/{employee.Id}", employee);
    }
    public async Task DeleteEmployeeAsync(int id)
    {
        await _httpClient.DeleteAsync($"Employee/{id}");
    }

    // CRUD pour les agences
    public async Task<List<Agency>> GetAllAgencyAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Agency>>("Agency");
    }
    public async Task AddAgencyAsync(Agency agency)
    {
        await _httpClient.PostAsJsonAsync("Agency", agency);
    }
    public async Task UpdateAgencyAsync(Agency agency)
    {
        await _httpClient.PutAsJsonAsync($"Agency/{agency.Id}", agency);
    }
    public async Task DeleteAgencyAsync(int id)
    {
        await _httpClient.DeleteAsync($"Agency/{id}");
    }
    public async Task<bool> IsAgencyAssignedAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"Agency/isassigned/{id}");
    }

    // CRUD pour les services
    public async Task<List<Service>> GetAllServiceAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Service>>("Service");
    }
    public async Task AddServiceAsync(Service service)
    {
        await _httpClient.PostAsJsonAsync("Service", service);
    }
    public async Task UpdateServiceAsync(Service service)
    {
        await _httpClient.PutAsJsonAsync($"Service/{service.Id}", service);
    }
    public async Task DeleteServiceAsync(int id)
    {
        await _httpClient.DeleteAsync($"Service/{id}");
    }
    public async Task<bool> IsServiceAssignedAsync(int id)
    {
        return await _httpClient.GetFromJsonAsync<bool>($"Service/isassigned/{id}");
    }
}
