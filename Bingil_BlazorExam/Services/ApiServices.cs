namespace Bingil_BlazorExam.Services
{
    public class ApiServices
    {

        private readonly HttpClient _http;

        public ApiServices(HttpClient http)
        {
            _http = http;
        }
        public async Task<Models.Quote?> GetQuoteAsync()
        {
            try
            {
                var url = "https://motivational-spark-api.vercel.app/api/quotes/random";
                return await _http.GetFromJsonAsync<Models.Quote>(url);
            }
            catch (HttpRequestException)
            {
                return null;
            }
        }
    }
}