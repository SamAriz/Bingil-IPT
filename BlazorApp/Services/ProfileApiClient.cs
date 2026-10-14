using BlazorApp1.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;

namespace BlazorApp1.Services
{
    // Calls this app's OWN /api/profile endpoint over HTTP.
    // It works out the app's own address at runtime, so it still works in
    // Codespaces or on a different port, with nothing hardcoded.
    public class ProfileApiClient
    {
        private readonly IHttpClientFactory _factory;
        private readonly IServer _server;

        public ProfileApiClient(IHttpClientFactory factory, IServer server)
        {
            _factory = factory;
            _server = server;
        }

        public async Task<ProfileInfo?> GetProfileAsync()
        {
            return await CreateClient().GetFromJsonAsync<ProfileInfo>("api/profile");
        }

        public async Task<DetailsInfo?> GetDetailsAsync()
        {
            return await CreateClient().GetFromJsonAsync<DetailsInfo>("api/details");
        }

        private HttpClient CreateClient()
        {
            var http = _factory.CreateClient("SelfApi");
            http.BaseAddress = new Uri(GetOwnAddress());
            return http;
        }

        private string GetOwnAddress()
        {
            var addresses = _server.Features.Get<IServerAddressesFeature>()?.Addresses
                            ?? Array.Empty<string>();

            // Prefer https (avoids the http -> https redirect), otherwise the first address.
            var address = addresses.FirstOrDefault(a => a.StartsWith("https://"))
                          ?? addresses.FirstOrDefault()
                          ?? "http://localhost:5210";

            return address.Replace("[::]", "localhost")
                          .Replace("0.0.0.0", "localhost")
                          .Replace("+", "localhost")
                          .Replace("*", "localhost");
        }
    }
}
