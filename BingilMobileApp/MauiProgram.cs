using BingilMobileApp.Services;
using Microsoft.Extensions.Logging;

namespace BingilMobileApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

            // One shared HttpClient for the external APIs (Pokemon, IP lookup, Cards)
            builder.Services.AddSingleton(_ =>
            {
                var http = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
                http.DefaultRequestHeaders.UserAgent.ParseAdd("BingilMobileApp/1.0");
                return http;
            });
            builder.Services.AddScoped<ApiService>();
            builder.Services.AddSingleton<StudentInfoApi>();

#if DEBUG
    		builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
