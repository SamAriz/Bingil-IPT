using BingilAPI.Models;

namespace BingilAPI.Services
{
    public class ApiService
    {
        private const string StudentApiBase = "https://localhost:7266/api/Student";
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

        //Own API
        // FULL NAME
        public async Task<string?> GetStudentFullNameAsync()
        {
            try { return await _http.GetStringAsync($"{StudentApiBase}/fullname"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<bool> SetStudentFullNameAsync(string fullName)
        {
            try { var r = await _http.PostAsJsonAsync($"{StudentApiBase}/fullname", fullName); return r.IsSuccessStatusCode; }
            catch (HttpRequestException) { return false; }
        }

        // ID NO
        public async Task<string?> GetStudentIdNoAsync()
        {
            try { return await _http.GetStringAsync($"{StudentApiBase}/idno"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<bool> SetStudentIdNoAsync(string idNo)
        {
            try { var r = await _http.PostAsJsonAsync($"{StudentApiBase}/idno", idNo); return r.IsSuccessStatusCode; }
            catch (HttpRequestException) { return false; }
        }

        // PROGRAM
        public async Task<string?> GetStudentProgramAsync()
        {
            try { return await _http.GetStringAsync($"{StudentApiBase}/program"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<bool> SetStudentProgramAsync(string program)
        {
            try { var r = await _http.PostAsJsonAsync($"{StudentApiBase}/program", program); return r.IsSuccessStatusCode; }
            catch (HttpRequestException) { return false; }
        }

        // BIRTHDATE
        public async Task<string?> GetStudentBirthDateAsync()
        {
            try { return await _http.GetStringAsync($"{StudentApiBase}/birthdate"); }
            catch (HttpRequestException) { return null; }
        }

        public async Task<bool> SetStudentBirthDateAsync(DateTime birthDate)
        {
            try { var r = await _http.PostAsJsonAsync($"{StudentApiBase}/birthdate", birthDate); return r.IsSuccessStatusCode; }
            catch (HttpRequestException) { return false; }
        }

        // AGE
        public async Task<int?> GetStudentAgeAsync()
        {
            try { return await _http.GetFromJsonAsync<int>($"{StudentApiBase}/age"); }
            catch (HttpRequestException) { return null; }
        }
        public async Task<StudentInfo> GetFullStudentInfoAsync()
        {
            var info = new StudentInfo();
            try
            {
                info.FullName = await _http.GetStringAsync($"{StudentApiBase}/fullname");
                info.IdNo = await _http.GetStringAsync($"{StudentApiBase}/idno");
                info.Program = await _http.GetStringAsync($"{StudentApiBase}/program");
                var birth = await _http.GetStringAsync($"{StudentApiBase}/birthdate");
                info.BirthDate = birth ?? string.Empty;
                info.Age = await _http.GetFromJsonAsync<int?>($"{StudentApiBase}/age") ?? 0;
            }
            catch (HttpRequestException)
            {
                // leave defaults if something fails
            }
            return info;
        }
    }
}