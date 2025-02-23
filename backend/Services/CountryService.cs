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

	public Country CreateCountry(Country country)
	{
		var blankStat = new InternetStatistic { CountryCode = country.Code };

		country.InternetStatistics = new List<InternetStatistic> { blankStat };
		_context.Countries.Add(country);
		_context.SaveChanges();
		return country;
	}

	public InternetStatistic UpdateWBRate(string code, decimal newRate)
	{
        if (newRate < 0 || newRate > 100)
            throw new ArgumentException("Rate must be between 0 and 100.");

        var stat = _context.InternetStatistics.Find(code);
        if (stat == null) return null;

        stat.PercentWB = newRate;
        stat.YearWB = DateTime.Now.Year;

        _context.SaveChanges();
        return stat;
    }
}
