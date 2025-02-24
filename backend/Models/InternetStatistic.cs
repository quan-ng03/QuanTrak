namespace backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class InternetStatistic
{
	[Key]
	[Required]
	[ForeignKey(nameof(Country))]
	public string? CountryCode { get; set; }

	public decimal? PercentWB { get; set; }
	public int? YearWB { get; set; }

	public decimal? PercentITU { get; set; }
	public int? YearITU { get; set; }

	public long? PopulationCIA { get; set; }
	public int? YearCIA { get; set; }

	[JsonIgnore] // This is just to prevent the infinite loop of references
	public Country? Country { get; set; }
}