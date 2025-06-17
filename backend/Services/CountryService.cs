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
	
	private List<CountryRankingDto> BuildCountryRankingList()
	{
		var countries = _context.Countries
			.Include(c => c.InternetStatistics)
			.AsNoTracking()
			.ToList();

		var rankedList = new List<CountryRankingDto>();
		var unrankedList = new List<CountryRankingDto>();

		foreach (var c in countries)
		{
			var stat = c.InternetStatistics.FirstOrDefault(s => s.PopulationCIA.HasValue && s.PercentWB.HasValue);

			var dto = new CountryRankingDto
			{
				Code = c.Code,
				CountryName = c.Name,
				Population = stat?.PopulationCIA ?? 0,
				PercentWB = stat?.PercentWB ?? 0,
				CalculatedInternetUsers = (stat?.PopulationCIA ?? 0) * ((stat?.PercentWB ?? 0) / 100),
				Rank = 0 // temp default
			};

			if (stat != null)
				rankedList.Add(dto);
			else
				unrankedList.Add(dto);
		}

		// Sort and assign ranks to valid entries
		rankedList = rankedList
			.OrderByDescending(dto => dto.CalculatedInternetUsers)
			.ToList();

		for (int i = 0; i < rankedList.Count; i++)
			rankedList[i].Rank = i + 1;

		return rankedList.Concat(unrankedList).ToList();
	}
	
	public List<CountryRankingDto> GetFullCountryRankingList()
	{
		return BuildCountryRankingList();
	}
	
	public List<CountryRankingDto> GetTopRankedCountries(int count)
	{
		return BuildCountryRankingList()
			.Where(dto => dto.Rank > 0)
			.Take(count)
			.ToList();
	}
	
	public CountryRankingDto? GetCountryRankingByCode(string code)
	{
		return BuildCountryRankingList()
			.FirstOrDefault(dto => dto.Code == code);
	}
}
