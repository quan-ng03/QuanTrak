using System.ComponentModel.DataAnnotations;

namespace frontend.Models
{
    public class Country
    {
        [Key]
        [Required]
        public string? Code { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        public string? Region { get; set; }
        public List<InternetStatistic>? InternetStatistics { get; set; } // One-to-many

    }

}
