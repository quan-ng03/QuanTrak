using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace backend.Services;

public class CountryService
{
	private readonly CountryContext _context;

	public CountryService(CountryContext context)
	{
		_context = context;
	}

	// Return a list of country names
	public IEnumerable<string> GetAll()
	{
		return _context.Countries
			.Select(p => p.Name!)
			.AsNoTracking()
			.ToList();
	}

	// Return all countries WB data sorted by rate
	public IEnumerable<Country> GetCountriesDetails()
	{
		var countries = _context.Countries
			.Include(c => c.InternetStatistics)
			.AsNoTracking()
			.OrderByDescending(c => c.InternetStatistics.FirstOrDefault().PercentWB)
			.ToList();
		return countries;
	}

	// Return top 10 countries with highest WB Rates
	public IEnumerable<Country> GetTop10Countries ()
	{
		var topCountries = _context.Countries
            .Include(c => c.InternetStatistics)
            .AsNoTracking()
            .OrderByDescending(c => c.InternetStatistics.FirstOrDefault().PercentWB)
            .Take(10)
            .ToList();
        return topCountries;
	}

	// Return a country's internet statistics
	public Country? GetByName(string name)
	{
		return _context.Countries
			.Include(p => p.InternetStatistics)
			.AsNoTracking()
			.SingleOrDefault(p => p.Name == name);
	}

	// Create a new country
	public Country CreateCountry(Country country)
	{
		var blankStat = new InternetStatistic { CountryCode = country.Code };

		country.InternetStatistics = new List<InternetStatistic> { blankStat };
		_context.Countries.Add(country);
		_context.SaveChanges();
		return country;
	}

	// Update the WB Rate of an existing Country
	public InternetStatistic UpdateWBRate(string code, decimal newRate)
	{
        if (newRate < 0 || newRate > 100)
            throw new ArgumentException("Rate must be between 0 and 100.");
        // Find the country Code
        var stat = _context.InternetStatistics.Find(code);
        if (stat == null) return null;
        
        // Update the rate and the year
        stat.PercentWB = newRate;
        stat.YearWB = DateTime.Now.Year;

        _context.SaveChanges();
        return stat;
    }

    public CountryRankingDto? GetCountryRankingByCode(string code)
    {
        // Get all countries with at least one InternetStatistic that has both PopulationCIA and PercentWB data.
        var countries = _context.Countries
            .Include(c => c.InternetStatistics)
            .AsNoTracking()
            .Where(c => c.InternetStatistics.Any(s => s.PopulationCIA.HasValue && s.PercentWB.HasValue))
            .ToList();

        // Transform each country into a DTO with the calculated value.
        var rankingList = countries.Select(c =>
        {
            var stat = c.InternetStatistics.First(s => s.PopulationCIA.HasValue && s.PercentWB.HasValue);
            long population = stat.PopulationCIA.Value;
            decimal percent = stat.PercentWB.Value;
            decimal calculatedUsers = population * (percent / 100);

            return new CountryRankingDto
            {
                Code = c.Code,
                CountryName = c.Name,
                Population = population,
                PercentWB = percent,
                CalculatedInternetUsers = calculatedUsers
            };
        })
        // Order in descending order by calculated internet users.
        .OrderByDescending(dto => dto.CalculatedInternetUsers)
        .ToList();

        // Assign ranking numbers.
        for (int i = 0; i < rankingList.Count; i++)
        {
            rankingList[i].Rank = i + 1;
        }

        return rankingList.FirstOrDefault(dto => dto.Code == code);
    }


}
