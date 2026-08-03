using BingilAPI.Models;

namespace BingilAPI.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;

        public ApiService(HttpClient http)
        {
            _http = http;
        }

        // 1. PokeAPI — Pokemon lookup by name or id
        public async Task<PokemonResult?> GetPokemonAsync(string nameOrId)
        {
            try
            {
                string url = $"https://pokeapi.co/api/v2/pokemon/{nameOrId.ToLower().Trim()}";
                return await _http.GetFromJsonAsync<PokemonResult>(url);
            }
            catch (HttpRequestException)
            {
                return null; // not found or bad name
            }
        }

        // 2. ip-api.com — IP address geolocation lookup
        public async Task<IpLookupResult?> GetIpLookupAsync(string ip)
        {
            string url = $"http://ip-api.com/json/{ip.Trim()}";
            return await _http.GetFromJsonAsync<IpLookupResult>(url);
        }

        // 3. Deck of Cards — draw one random card
        public async Task<PlayingCard?> DrawCardAsync()
        {
            var result = await _http.GetFromJsonAsync<DrawCardResponse>("https://deckofcardsapi.com/api/deck/new/draw/?count=1");
            return result?.cards.FirstOrDefault();
        }
    }
}
