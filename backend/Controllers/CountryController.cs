using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{
	private readonly CountryService _service;

	public CountryController(CountryService service)
	{
		_service = service;
	}

	// GET all countries
	[HttpGet]
	public IEnumerable<string>GetAll()
	{
		return _service.GetAll();
	}

	// GET all countries WB data
	[HttpGet("details")]
	public IEnumerable<Country> GetContriesDetails()
	{
		return _service.GetCountriesDetails();
	}


    // GET a certain country
    [HttpGet("{name}")]
	public ActionResult<Country> GetByName(string name)
	{
		var country = _service.GetByName(name);
		if (country is not null)
		{
			return Ok(country);
		}
		else
		{
			return NotFound();
		}
	}

	// UPDATE a country WB rate
	[HttpPut("{code}")]
	public ActionResult<InternetStatistic> UpdateWBRate(string code, [FromBody]decimal newRate)
	{
        try
        {
            var updated = _service.UpdateWBRate(code, newRate);
            if (updated == null) return NotFound("Statistic not found.");

            return Ok(updated);
        }
        catch (ArgumentException ex)
        {
            // Invalid input (rate out of range)
            return BadRequest(ex.Message);
        }
    }

	
	[HttpGet("ranking")]
	public IActionResult GetAllRankings()
	{
		var list = _service.GetFullCountryRankingList();
		return Ok(list);
	}
	
    // GET the ranking of a country
    [HttpGet("ranking/{code}")]
    public IActionResult GetCountryRanking(string code)
    {
	    var dto = _service.GetCountryRankingByCode(code);
	    return dto == null ? NotFound($"Country with code {code} not found.") : Ok(dto);
    }
    
    [HttpGet("ranking/top/{count}")]
    public IActionResult GetTopRankings(int count)
    {
	    var list = _service.GetTopRankedCountries(count);
	    return Ok(list);
    }
}