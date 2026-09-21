// Root using directives for the Blazor app and data models
using BingilDashboard.Components;
using BingilDashboard.Data;
using Microsoft.AspNetCore.Components;

// Build the web application host and DI container
var builder = WebApplication.CreateBuilder(args);

// Register services required for interactive Razor/Blazor server components
builder.Services
    .AddRazorComponents()
    .AddInteractiveServerComponents();

// Provide an HttpClient configured with the app base URI so components can call the server
builder.Services.AddScoped(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HttpClient { BaseAddress = new Uri(nav.BaseUri) };
});

// Add minimal API explorer and Swagger generation (optional developer tooling)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Create the application pipeline
var app = builder.Build();

// Configure middleware: enable Swagger in development, and production error/HSTS otherwise
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

// Common middleware
app.UseHttpsRedirection();
app.UseAntiforgery();

// Serve framework/static assets required by Blazor and map the root component
app.MapStaticAssets();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();

// Minimal API endpoint that returns the in-memory profile data as JSON
app.MapGet("/api/profile", () => Results.Ok(ProfileStore.Get())).WithName("GetProfile");

// Start the web server
app.Run();
