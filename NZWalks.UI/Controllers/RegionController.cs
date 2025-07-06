using Microsoft.AspNetCore.Mvc;
using NZWalks.UI.Models;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace NZWalks.UI.Controllers
{
    public class RegionController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            List<RegionDto> response = new List<RegionDto>();
            try
            {
                var client = httpClientFactory.CreateClient();
                var httpResponseMessage = await client.GetAsync("https://localhost:7167/api/regions");

                httpResponseMessage.EnsureSuccessStatusCode();

                response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionDto>>());


            }
            catch (Exception ex)
            {

                //Log the exception (ex) here if needed
            }

            return View(response);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel addRegionViewModel)
        {
            var client = httpClientFactory.CreateClient();
            
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7167/api/regions"),
                Content = new StringContent(
                    JsonSerializer.Serialize(addRegionViewModel),
                    Encoding.UTF8,
                    "application/json")
                };
            var httpResponseMessage = await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if(response is not null)
            {
                return RedirectToAction("Index" , "Regions");
            }
            return View();
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            ViewBag.Id = id;
            var client = httpClientFactory.CreateClient();
            var response = await client.GetFromJsonAsync<RegionDto>($"https://localhost:7167/api/regions/{id.ToString()}");
            
            if (response is not null)
            {
                return View(response);
            }
            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionDto request)
        {
            var client = httpClientFactory.CreateClient();
            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7167/api/regions/{request.Id}"),
                Content = new StringContent(
                    JsonSerializer.Serialize(request),
                    Encoding.UTF8,
                    "application/json")
            };
            var httpResponseMessage =  await client.SendAsync(httpRequestMessage);
            httpResponseMessage.EnsureSuccessStatusCode();

            var response = await httpResponseMessage.Content.ReadFromJsonAsync<RegionDto>();

            if (response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionDto request)
        {
            try
            {

            var client = httpClientFactory.CreateClient();
            var httpResponseMessage =  await client.DeleteAsync($"https://localhost:7167/api/regions/{request.Id}");
            httpResponseMessage.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Regions");
            }
            catch (Exception ex)
            {
                //Log the exception (ex) here if needed
            }

            return View("Edit");

        }
    }
}
