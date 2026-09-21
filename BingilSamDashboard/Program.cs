using BingilSamDashboard.Components;
using BingilSamDashboard.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

// In-memory Profile data with sensible defaults
var profile = new ProfileInfo();

app.MapGet("/api/Profile", () => Results.Ok(profile));
app.MapGet("/api/Profile/name", () => Results.Ok(profile.FullName));
app.MapGet("/api/Profile/intro", () => Results.Ok(profile.Introduction));
app.MapGet("/api/Profile/skills", () => Results.Ok(profile.Skills));
app.MapGet("/api/Profile/contact", () => Results.Ok(new { profile.Phone, profile.Email, profile.Location }));

app.MapPost("/api/Profile/name", async (HttpContext ctx) =>
{
    var req = ctx.Request;

    // Allow reading the body multiple times
    req.EnableBuffering();

    string body;
    using (var reader = new System.IO.StreamReader(req.Body, System.Text.Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true))
    {
        body = await reader.ReadToEndAsync();
        req.Body.Position = 0;
    }

    string? name = null;
    var contentType = req.ContentType ?? string.Empty;

    if (contentType.Contains("application/json", System.StringComparison.OrdinalIgnoreCase))
    {
        try
        {
            name = System.Text.Json.JsonSerializer.Deserialize<string>(body);
        }
        catch (System.Text.Json.JsonException)
        {
            // ignore and fall back to other strategies
        }
    }

    if (string.IsNullOrEmpty(name) && req.HasFormContentType)
    {
        if (req.Form.TryGetValue("name", out var v))
            name = v;
    }

    if (string.IsNullOrEmpty(name))
    {
        // Plain text or raw body fallback
        name = body;
    }

    name = (name ?? string.Empty).Trim().Trim('"');
    if (string.IsNullOrWhiteSpace(name)) return Results.BadRequest("Name cannot be empty.");
    profile.FullName = name;
    return Results.Ok(profile.FullName);
});

app.MapPost("/api/Profile/intro", async (HttpContext ctx) =>
{
    var intro = await ctx.Request.ReadFromJsonAsync<string>();
    if (intro is null) return Results.BadRequest();
    profile.Introduction = intro;
    return Results.Ok(profile.Introduction);
});

app.MapPost("/api/Profile/skills", async (HttpContext ctx) =>
{
    var skills = await ctx.Request.ReadFromJsonAsync<List<string>>();
    if (skills is null) return Results.BadRequest();
    profile.Skills = skills;
    return Results.Ok(profile.Skills);
});

// (Student endpoints removed — using Profile endpoints instead)

app.Run();
