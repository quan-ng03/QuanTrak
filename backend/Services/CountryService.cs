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

	public IEnumerable<string> GetAll()
	{
		return _context.Countries
			.Select(p => p.Name!)
			.AsNoTracking()
			.ToList();
	}

	public Country? GetByName(string name)
	{
		return _context.Countries
			.Include(p => p.InternetStatistics)
			.AsNoTracking()
			.SingleOrDefault(p => p.Name == name);
	}
}
