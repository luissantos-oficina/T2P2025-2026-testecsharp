using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OficinaApp.Models;
using System.Text.Json;

namespace OficinaApp.Pages;

    public class InfodragonballModel : PageModel
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public InfodragonballModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public Characters Personagem { get; set; }

        public async Task<IActionResult> OnGetAsync(string cod)
        {

            var client = _httpClientFactory.CreateClient("RestDragonBall");

            var response = await client.GetAsync("api/characters/" + cod);

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var artigoResponse = JsonSerializer.Deserialize<Characters>(json, options);


            Personagem = new Characters
            {
                id = artigoResponse.id,
                name = artigoResponse.name,
                image = artigoResponse.image,
                description = artigoResponse.description,
                affiliation = artigoResponse.affiliation
            };                                  

            return Page();
        }
    }

