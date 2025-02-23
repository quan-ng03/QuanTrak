using System.Net.Http;
using System.Net.Http.Json;
using frontend.Models;

public class CountryService
{
    private readonly HttpClient _httpClient;

    public CountryService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // Gets full details for all countries, sorted by WB rate
    public async Task<List<Country>> GetCountriesDetailsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Country>>("api/Country/details");
    }

    // Gets top 10 countries for pie chart
    public async Task<List<Country>> GetTop10CountriesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Country>>("api/Country/top10");
    }

    // Gets a list of all country names (if needed for dropdown)
    public async Task<List<string>> GetCountryNamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("api/Country");
    }

    // Gets detailed summary for a given country
    public async Task<Country> GetCountryByNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<Country>($"api/Country/{name}");
    }
}

