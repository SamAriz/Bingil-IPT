using BlazorApp1.Components;
using BingilAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Register HttpClient factory and register ApiService as a typed client so it receives an HttpClient
builder.Services.AddHttpClient();
// Register ApiService with typed client and allow DI to provide ILogger
builder.Services.AddHttpClient<BingilAPI.Services.ApiService>(client =>
{
    // Fail fast instead of leaving a button on "Searching..." for the default 100 seconds
    client.Timeout = TimeSpan.FromSeconds(15);
    // .NET sends no User-Agent by default and some public APIs reject that
    client.DefaultRequestHeaders.UserAgent.ParseAdd("BlazorApp1/1.0");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
