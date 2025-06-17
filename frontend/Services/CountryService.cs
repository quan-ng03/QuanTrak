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
    // Gets a list of all country names (if needed for dropdown)
    public async Task<List<string>> GetCountryNamesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<string>>("api/Country");
    }

    // Gets countries by region from the backend.
    public async Task<List<Country>> GetCountriesByRegionAsync(string region)
    {
        return await _httpClient.GetFromJsonAsync<List<Country>>($"api/Country/region/{region}");
    }

    // Gets detailed summary for a given country
    public async Task<Country> GetCountryByNameAsync(string name)
    {
        return await _httpClient.GetFromJsonAsync<Country>($"api/Country/{name}");
    }

    public async Task<InternetStatistic> UpdateWBRateAsync(string code, decimal newRate)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/Country/{code}", newRate);
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<InternetStatistic>();
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new Exception(error);
        }
    }

    public async Task<CountryRankDTO?> GetCountryRankingAsync(string code)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<CountryRankDTO>($"api/Country/ranking/{code}");
        }
        catch (HttpRequestException ex)
        {
            // Optionally handle 404s gracefully
            return null;
        }
    }
    
    public async Task<List<CountryRankDTO>> GetTopRankedCountriesAsync(int count = 10)
    {
        return await _httpClient.GetFromJsonAsync<List<CountryRankDTO>>($"api/Country/ranking/top/{count}");
    }
    
    public async Task<List<CountryRankDTO>> GetAllRankingsAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<CountryRankDTO>>("api/Country/ranking");
    }

}

