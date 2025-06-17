namespace backend.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


public class Country
{
	[Key]
	[Required]
	[Column(TypeName = "varchar(10)")]
	public string Code { get; set; } = default!;

	[Required]
	[MaxLength(100)]
	[Column(TypeName = "varchar(100)")]
	public string Name { get; set; } = default!;

	public List<InternetStatistic> InternetStatistics { get; set; } = new();
}

