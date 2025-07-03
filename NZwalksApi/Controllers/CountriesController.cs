using Microsoft.AspNetCore.Mvc;
using NZwalksApi.Models.DTO;

namespace NZwalksApi.Controllers
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    public class CountriesController : Controller
    {
        [MapToApiVersion("1.0")]
        [HttpGet]
        public IActionResult GetV1()
        {
            var countriesDomainModel = new List<(int Id, string Name)>
            {
                (1, "New Zealand"),
                (2, "Australia"),
                (3, "United States"),
                (4, "Canada"),
                (5, "United Kingdom")
            };
            var response = new List<CountryDtov1>();
            foreach (var country in countriesDomainModel)
            {
                response.Add(new CountryDtov1
                {
                    Id = country.Id,
                    Name = country.Name,
                });
            }

            return Ok(response);
        }
        [MapToApiVersion("2.0")]
        [HttpGet]

        public IActionResult GetV2()
        {
            var countriesDomainModel = new List<(int Id, string Name)>
            {
                (1, "India"),
                (2, "China"),
                (3, "Japan"),
                (4, "South Korea"),
                (5, "Thailand")
            };
            var response = new List<CountryDtov2>();
            foreach (var country in countriesDomainModel)
            {
                response.Add(new CountryDtov2
                {
                    Id = country.Id,
                    CountryName = country.Name,
                });
            }

            return Ok(response);
        }


    }
}
