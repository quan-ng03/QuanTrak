namespace backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

public class InternetStatistic
{
	[Key]
	[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
	public int Id { get; set; }

	[ForeignKey(nameof(Country))]
	[Column(TypeName = "varchar(10)")]
	public string CountryCode { get; set; } = default!;

	[Column(TypeName = "numeric")]
	public decimal? PercentWB { get; set; }

	public int? YearWB { get; set; }

	[Column(TypeName = "numeric")]
	public decimal? PercentITU { get; set; }

	public int? YearITU { get; set; }

	public long? PopulationCIA { get; set; }
	public int? YearCIA { get; set; }

	[JsonIgnore]
	public Country Country { get; set; } = default!;
}
