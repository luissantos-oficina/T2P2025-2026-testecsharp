using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OficinaApp.Models;

namespace OficinaApp.Pages;

    public class InfopaisModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public InfopaisModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Pais Infopais { get; set; }
        public string CodigoPais { get; set; }


        public async Task<IActionResult> OnGetAsync(string cod)
        {
            CodigoPais = cod;

            var client = _httpClientFactory.CreateClient("RestCountries");

            var response = await client.GetAsync("v3.1/alpha/" + cod);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var artigoResponse = JsonSerializer.Deserialize<List<CountryApiResponse>>(json, options)?.FirstOrDefault();

            Infopais = new Pais
            {
                Cca2 = artigoResponse.cca2,
                OfficialName = artigoResponse.name?.official,
                FlagUrl = artigoResponse.flags?.png
            };


            return Page();
        }
    }

