using System.Globalization;
using backend.Data;
using backend.Models;
using Microsoft.VisualBasic.FileIO;

namespace backend.Data;

public class DbInitializer
{
	public static void Initialize(CountryContext context)
	{
		if (!context.Countries.Any())
		{
			using (var parser = new TextFieldParser("Seed/countries.csv")) // Seed the Country table
			{
				parser.SetDelimiters(",");
				parser.ReadLine(); // Skip header
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					context.Countries.Add(new Country { Name = fields[0], Code = fields[1] } );
				}
				context.SaveChanges();
			}
		}

		if (!context.InternetStatistics.Any())
		{
			using (var parser = new TextFieldParser("Seed/country_internet_statistics.csv")) // Seed the internet statistics table
			{
				parser.SetDelimiters(",");
				parser.ReadLine();
				while (!parser.EndOfData)
				{
					string[] fields = parser.ReadFields();
					if (fields.Length < 7) continue; // Skip malformed rows
					string countryCode = fields[0];
					if (!context.Countries.Any(c => c.Code == countryCode)) continue;

					context.InternetStatistics.Add(new InternetStatistic
					{
						CountryCode = countryCode,
						PercentWB = string.IsNullOrEmpty(fields[1]) ? null : decimal.Parse(fields[1], CultureInfo.InvariantCulture),
						YearWB = string.IsNullOrEmpty(fields[2]) ? null : int.Parse(fields[2]),
						PercentITU = string.IsNullOrEmpty(fields[3]) ? null : decimal.Parse(fields[3], CultureInfo.InvariantCulture),
						YearITU = string.IsNullOrEmpty(fields[4]) ? null : int.Parse(fields[4]),
						PopulationCIA = string.IsNullOrEmpty(fields[5]) ? null : long.Parse(fields[5]),
						YearCIA = string.IsNullOrEmpty(fields[6]) ? null : int.Parse(fields[6])
					});
				}
				context.SaveChanges();
			}
		}
	}
}