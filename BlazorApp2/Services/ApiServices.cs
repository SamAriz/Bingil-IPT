using BlazorApp2.Models;

namespace BlazorApp2.Services
{
    public class ApiService
    {
        private readonly HttpClient _http;
        private const string StudentApiBase = "https://localhost:7266/api/Student"; // confirm this matches your Student API's port

        public ApiService(HttpClient http)
        {
            _http = http;
        }

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
                info.FullName = await _http.GetStringAsync($"{StudentApiBase}/fullname") ?? "";
                info.IdNo = await _http.GetStringAsync($"{StudentApiBase}/idno") ?? "";
                info.Program = await _http.GetStringAsync($"{StudentApiBase}/program") ?? "";
                info.BirthDate = await _http.GetStringAsync($"{StudentApiBase}/birthdate") ?? "";
                info.Age = await _http.GetFromJsonAsync<int>($"{StudentApiBase}/age");
            }
            catch (HttpRequestException)
            {
                // leave defaults if something fails
            }
            return info;
        }
    }
}