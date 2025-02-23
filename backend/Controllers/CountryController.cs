using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
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

    // GET top 10 countires WB rate
    [HttpGet("top10")]
    public IEnumerable<Country> GetTop10Countries()
    {
		return _service.GetTop10Countries();
    }


    // GET a certain country
    [HttpGet("{name}")]
	public ActionResult<Country> GetByName(string name)
	{
		var country = _service.GetByName(name);
		if (country is not null)
		{
			return country;
		}
		else
		{
			return NotFound();
		}
	}

	// CREATE a new country name and code
	[HttpPost]
	public ActionResult<Country> Create([FromBody]Country country)
	{
		if (!ModelState.IsValid) return BadRequest(ModelState);

		if (_service.GetByName(country.Name!) != null)
			return Conflict("Country already exist!");
		
		var newCountry = _service.CreateCountry(country);
		return CreatedAtAction(nameof(GetByName), new { name = newCountry.Name }, newCountry);
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
}