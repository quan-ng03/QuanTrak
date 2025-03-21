﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace frontend.Models
{

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

        [JsonIgnore]
        public Country? Country { get; set; }
    }
}
