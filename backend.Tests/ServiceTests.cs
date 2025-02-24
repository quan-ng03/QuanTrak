using System;
using System.Collections.Generic;
using System.Linq;
using backend.Data;
using backend.Models;
using backend.Services;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace backend.Tests
{
    public class CountryServiceTests : IDisposable
    {
        private readonly CountryContext _context;
        private readonly CountryService _service;

        public CountryServiceTests()
        {
            var options = new DbContextOptionsBuilder<CountryContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new CountryContext(options);

            SeedData();

            _service = new CountryService(_context);
        }

        private void SeedData()
        {
            // Create a few sample countries with internet statistics.
            var countries = new List<Country>
            {
                new Country
                {
                    Code = "US",
                    Name = "United States",
                    InternetStatistics = new List<InternetStatistic>
                    {
                        new InternetStatistic { CountryCode = "US", PercentWB = 90, YearWB = 2020 }
                    }
                },
                new Country
                {
                    Code = "CA",
                    Name = "Canada",
                    InternetStatistics = new List<InternetStatistic>
                    {
                        new InternetStatistic { CountryCode = "CA", PercentWB = 80, YearWB = 2020 }
                    }
                },
                new Country
                {
                    Code = "MX",
                    Name = "Mexico",
                    InternetStatistics = new List<InternetStatistic>
                    {
                        new InternetStatistic { CountryCode = "MX", PercentWB = 70, YearWB = 2020 }
                    }
                }
            };

            _context.Countries.AddRange(countries);
            _context.SaveChanges();
        }

        [Fact]
        public void GetAll_ReturnsAllCountryNames()
        {
            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Contains("United States", result);
            Assert.Contains("Canada", result);
            Assert.Contains("Mexico", result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetAll_NoCountries_ReturnsEmptyList()
        {
            // Arrange - Clear the database
            _context.Countries.RemoveRange(_context.Countries);
            _context.SaveChanges();

            // Act
            var result = _service.GetAll();

            // Assert
            Assert.Empty(result);
        }


        [Fact]
        public void GetCountriesDetails_ReturnsCountriesSortedByWBRateDescending()
        {
            // Act
            var result = _service.GetCountriesDetails().ToList();

            // Assert: The United States (90%) should be first, then Canada (80%), then Mexico (70%)
            Assert.Equal("United States", result[0].Name);
            Assert.Equal("Canada", result[1].Name);
            Assert.Equal("Mexico", result[2].Name);
        }

        [Fact]
        public void GetTop10Countries_ReturnsAtMost10Countries()
        {
            // Act
            var result = _service.GetTop10Countries().ToList();

            // Assert
            Assert.True(result.Count <= 10);
            Assert.Equal(3, result.Count); // Expected to have 3 total countries
        }

        [Fact]
        public void GetByName_ReturnsCorrectCountry()
        {
            // Act
            var country = _service.GetByName("Canada");

            // Assert
            Assert.NotNull(country);
            Assert.Equal("CA", country.Code);
        }

        [Fact]
        public void GetByName_CountryDoesNotExist_ReturnsNull()
        {
            // Act
            var result = _service.GetByName("NonExistentCountry");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCountriesDetails_CountryWithoutStatistics_DoesNotThrowError()
        {
            // Arrange
            var newCountry = new Country { Code = "JP", Name = "Japan" };
            _context.Countries.Add(newCountry);
            _context.SaveChanges();

            // Act
            var result = _service.GetCountriesDetails();

            // Assert
            Assert.Contains(result, c => c.Name == "Japan");
        }


        [Fact]
        public void UpdateWBRate_ValidRate_UpdatesStatistic()
        {
            // Act: For an existing country (US), update the rate to 95.
            var stat = _service.UpdateWBRate("US", 95);

            // Assert
            Assert.NotNull(stat);
            Assert.Equal(95, stat.PercentWB);
            Assert.Equal(DateTime.Now.Year, stat.YearWB);
        }

        [Fact]
        public void UpdateWBRate_NonExistentCountry_ReturnsNull()
        {
            // Act
            var result = _service.UpdateWBRate("XYZ", 50);

            // Assert
            Assert.Null(result);
        }


        [Fact]
        public void UpdateWBRate_InvalidRate_ThrowsArgumentException()
        {
            // Assert that negative rate throws exception
            Assert.Throws<ArgumentException>(() => _service.UpdateWBRate("US", -5));
            // Assert that rate greater than 100 throws exception
            Assert.Throws<ArgumentException>(() => _service.UpdateWBRate("US", 105));
        }

        [Fact]
        public void GetCountryRankingByCode_CountryWithoutValidData_ReturnsNull()
        {
            // Arrange
            var country = new Country
            {
                Code = "IN",
                Name = "India",
                InternetStatistics = new List<InternetStatistic>
        {
            new InternetStatistic { CountryCode = "IN", PercentWB = null, PopulationCIA = null }
        }
            };
            _context.Countries.Add(country);
            _context.SaveChanges();

            // Act
            var result = _service.GetCountryRankingByCode("IN");

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCountryRankingByCode_CountriesWithSameInternetUsers_CorrectlyRanks()
        {
            // Arrange
            var country1 = new Country
            {
                Code = "DE",
                Name = "Germany",
                InternetStatistics = new List<InternetStatistic>
        {
            new InternetStatistic { CountryCode = "DE", PopulationCIA = 1000000, PercentWB = 50 }
        }
            };

            var country2 = new Country
            {
                Code = "GB",
                Name = "United Kingdom",
                InternetStatistics = new List<InternetStatistic>
        {
            new InternetStatistic { CountryCode = "GB", PopulationCIA = 1000000, PercentWB = 50 }
        }
            };

            _context.Countries.AddRange(country1, country2);
            _context.SaveChanges();

            // Act
            var rank1 = _service.GetCountryRankingByCode("DE");
            var rank2 = _service.GetCountryRankingByCode("GB");

            // Assert
            Assert.NotNull(rank1);
            Assert.NotNull(rank2);
            Assert.Equal(rank1.CalculatedInternetUsers, rank2.CalculatedInternetUsers);
            Assert.NotEqual(rank1.Rank, rank2.Rank);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
