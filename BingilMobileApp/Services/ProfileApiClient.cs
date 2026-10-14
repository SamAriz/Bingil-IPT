using BingilMobileApp.Models;

namespace BingilMobileApp.Services
{
    // Your own "API" for the phone app.
    //
    // On the website this data is served by app.MapGet("/api/profile") and
    // app.MapGet("/api/details") and fetched over HTTP. A phone app has no web
    // server, so the same two calls live here as a local service instead.
    // The method names and return types are the same, so Profile.razor
    // is identical in both projects.
    public class ProfileApiClient
    {
        // Same as GET /api/profile
        public Task<ProfileInfo?> GetProfileAsync()
        {
            var profile = new ProfileInfo
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
            };
            return Task.FromResult<ProfileInfo?>(profile);
        }

        // Same as GET /api/details
        public Task<DetailsInfo?> GetDetailsAsync()
        {
            var details = new DetailsInfo
            {
                Age = 21,
                StudentId = "24-1217",
                School = "Southern Christian College",
                Course = "Information Technology", // EDIT: your exact program name
                Birthday = "02-06-05"
            };
            return Task.FromResult<DetailsInfo?>(details);
        }
    }
}
