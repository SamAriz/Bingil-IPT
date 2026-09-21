using BingilAPI.Models;
using System.Net;
using System.Text.Json;

namespace BingilAPI.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private readonly JsonSerializerOptions _jsonOptions = new() { PropertyNameCaseInsensitive = true };
        private readonly ILogger<ApiService> _logger;
        public string? LastError { get; private set; }

        public ApiService(HttpClient http, ILogger<ApiService> logger)
        {
            _http = http;
            _logger = logger;
        }

        // 1. PokeAPI — Pokemon lookup by name or id
        public async Task<PokemonResult?> GetPokemonAsync(string nameOrId)
        {
            LastError = null;
            if (string.IsNullOrWhiteSpace(nameOrId))
            {
                LastError = "Empty query.";
                return null;
            }
            var url = $"https://pokeapi.co/api/v2/pokemon/{nameOrId.ToLower().Trim()}";
            try
            {
                using var resp = await _http.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    LastError = DescribeHttpError(resp.StatusCode, "Pokemon not found — check the spelling.");
                    _logger.LogWarning("GetPokemonAsync failed: {Status} {Reason} {Content}", resp.StatusCode, resp.ReasonPhrase, content);
                    return null;
                }
                await using var stream = await resp.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<PokemonResult>(stream, _jsonOptions);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                _logger.LogError(ex, "GetPokemonAsync exception");
                return null;
            }
        }

        // 2. ipinfo.io — IP address geolocation lookup (HTTPS)
        public async Task<IpLookupResult?> GetIpLookupAsync(string ip)
        {
            LastError = null;
            var trimmed = (ip ?? string.Empty).Trim();
            var url = string.IsNullOrEmpty(trimmed) ? "https://ipinfo.io/json" : $"https://ipinfo.io/{trimmed}/json";
            try
            {
                using var resp = await _http.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    LastError = DescribeHttpError(resp.StatusCode, "IP lookup failed — check that the address is valid.");
                    _logger.LogWarning("GetIpLookupAsync failed: {Status} {Reason} {Content}", resp.StatusCode, resp.ReasonPhrase, content);
                    return null;
                }
                await using var stream = await resp.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<IpLookupResult>(stream, _jsonOptions);
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                _logger.LogError(ex, "GetIpLookupAsync exception");
                return null;
            }
        }

        // 3. Deck of Cards — draw one random card
        public async Task<PlayingCard?> DrawCardAsync()
        {
            var url = "https://deckofcardsapi.com/api/deck/new/draw/?count=1";
            try
            {
                LastError = null;
                using var resp = await _http.GetAsync(url);
                if (!resp.IsSuccessStatusCode)
                {
                    var content = await resp.Content.ReadAsStringAsync();
                    LastError = DescribeHttpError(resp.StatusCode, "Card service not found.");
                    _logger.LogWarning("DrawCardAsync failed: {Status} {Reason} {Content}", resp.StatusCode, resp.ReasonPhrase, content);
                    return null;
                }
                await using var stream = await resp.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<DrawCardResponse>(stream, _jsonOptions);
                return result?.cards?.FirstOrDefault();
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                _logger.LogError(ex, "DrawCardAsync exception");
                return null;
            }
        }

        // Short, readable message for the UI (full details still go to the log)
        private static string DescribeHttpError(HttpStatusCode code, string notFoundMessage) => code switch
        {
            HttpStatusCode.NotFound => notFoundMessage,
            HttpStatusCode.TooManyRequests => "Too many requests — wait a moment and try again.",
            _ => $"The service returned an error ({(int)code} {code}). Please try again."
        };
    }
}
