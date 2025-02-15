using backend.Services;
using backend.Models;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
	CountryService _service;

	public CountryController(CountryService service)
	{
		_service = service;
	}

	[HttpGet]
	public IEnumerable<string>GetAll()
	{
		return _service.GetAll();
	}

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
}