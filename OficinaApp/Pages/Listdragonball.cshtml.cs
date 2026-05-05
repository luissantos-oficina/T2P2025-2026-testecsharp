using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using OficinaApp.Models;
using System.Text.Json;

namespace OficinaApp.Pages;
    public class ListdragonballModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ListdragonballModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Characters> Personagens { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(string cod)
        {
            
            var client = _httpClientFactory.CreateClient("RestDragonBall");
            var response = await client.GetAsync("api/characters/");

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var lista = json.FirstOrDefault();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var dados = JsonSerializer.Deserialize<DragonBallList>(json, options);

                Personagens = dados.Items.Select(d => new Characters
                {
                    id = d.id,
                    name = d.name,
                    image = d.image
                }).ToList();
            }         

            return Page();
        }
    }

