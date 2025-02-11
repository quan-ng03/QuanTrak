using Microsoft.EntityFrameworkCore;
using backend.Models;

namespace backend.Data;

public class CountryContext : DbContext
{
	public CountryContext (DbContextOptions<CountryContext> options)
		: base(options)
		{ }

	public DbSet<Country> Countries => Set<Country>();

	public DbSet<InternetStatistic> internetStatistics => Set<InternetStatistic>();
}