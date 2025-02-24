public class CountryRankingDto
{
    public string? Code { get; set; }
    public string? CountryName { get; set; }
    public long Population { get; set; }
    public decimal PercentWB { get; set; }
    public decimal CalculatedInternetUsers { get; set; }
    public int Rank { get; set; }
}