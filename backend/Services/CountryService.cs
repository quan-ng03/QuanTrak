using backend.Models;
using backend.Data;
using Microsoft.EntityFrameworkCore;

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
