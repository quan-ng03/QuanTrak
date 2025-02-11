namespace backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.OpenApi.Models;

public class InternetStatistic
{

	public int Id { get; set; }

	[ForeignKey("Country")]
	public string? Code { get; set; }

	public int? YearWB { get; set; }
	public decimal? PercentWB { get; set; }

	public int? YearITU { get; set; }
	public decimal? PercentITU { get; set; }

	public int? YearCIA { get; set; }
	public long? PopulationCIA { get; set; }

}