namespace backend.Models;
using System.ComponentModel.DataAnnotations;

public class Country
{
	[Key]
	[Required]
	public string? Code { get; set; }

	[Required]
	[MaxLength(100)]
	public string? Name { get; set; }

	public List<InternetStatistic>? InternetStatistics { get; set; } // One-to-many
	
}
