using BlazorApp1.Components;
using BingilAPI.Services;
using BlazorApp1.Models;
using BlazorApp1.Services;

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

// ---- Profile API: named client + service the Profile page uses to call our own endpoint ----
var selfApi = builder.Services.AddHttpClient("SelfApi");
if (builder.Environment.IsDevelopment())
{
    // Dev only: the app calls itself on localhost, so accept the local dev certificate.
    selfApi.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    });
}
builder.Services.AddScoped<ProfileApiClient>();

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

// ================= THE API =================
// GET /api/profile  ->  returns your profile information as JSON.
// Intro text is NOT here; it stays on the page.
app.MapGet("/api/profile", () => new ProfileInfo
{
    Name = "Sam Ariz G. Bingil",
    Role = "IT Student",
    Contact = new ContactInfo
    {
        Phone = "+63 9776652741",
        Email = "sam@gmail.com",
        Address = "Pob. 5, Midsayap, Cotabato"
    },
    Skills = new List<string>
    {
        "I can make it", "Web Development", "Database Management",
        "SQL Database Management", "Linux/Unix Command line",
        "Python", "C#", "JAVA", "HTML", "CSS"
    },
    Languages = new List<string> { "English - Proficient", "Tagalog - Proficient" },
    Hobbies = new List<string>
    {
        "Eating", "Online Games", "Music",
        "Watching Movies", "Watching Anime", "Sleeping"
    },
    Weaknesses = new List<string> { "Selos" },
    Experiences = new List<string>
    {
        "HackforGov", "PSITS Java Coding",
        "CodeChum National Programming Challenge Season 2"
    },
    Descriptions = new List<string>
    {
        "Developed school projects using Java, Python, and C#",
        "Led functional teams...somehow"
    }
});

// GET /api/details  ->  your age, ID, school, course and birthday.
// This is its own API: the page loads it with a button, like the others.
app.MapGet("/api/details", () => new DetailsInfo
{
    Age = 21,
    StudentId = "24-1217",
    School = "Southern Christian College",
    Course = "Information Technology", // EDIT: your exact program name
    Birthday = "02-06-05"
});
// ===========================================
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
