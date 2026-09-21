using System;
using System.Collections.Generic;

namespace BingilDashboard.Data
{
    // Simple data model representing a user's profile.
    // This model is returned by the minimal API and consumed by the Blazor UI.
    public class Profile
    {
        // Full name of the person
        public string Name { get; set; } = string.Empty;

        // Numeric age
        public int Age { get; set; }

        // Identifier string (student ID in this sample)
        public string Id { get; set; } = string.Empty;

        // List of schools attended
        public List<string> Schools { get; set; } = new List<string>();

        // Course or program of study
        public string Course { get; set; } = string.Empty;

        // Birthday stored as DateOnly
        public DateOnly Birthday { get; set; }

        // Skill names
        public List<string> Skills { get; set; } = new List<string>();

        // Short introduction / about text
        public string Intro { get; set; } = string.Empty;
    }

    // Simple in-memory store that returns a sample Profile instance.
    // In a real app this would be backed by a database or external service.
    public static class ProfileStore
    {
        public static Profile Get() => new Profile
        {
            Name = "Sam Ariz Bingil",
            Age = 21,
            Id = "24-1217",
            Schools = new List<string>
            {
                "Elementary School Name",
                "High School Name",
                "University / College Name"
            },
            Course = "Bachelor of Science in Information Technology",
            Birthday = new DateOnly(2005, 2, 6),
            Skills = new List<string>
            {
                "C#",
                "Blazor",
                ".NET",
                "HTML/CSS",
                "JavaScript"
            },
            Intro = "Hi! I'm a passionate developer who loves building web apps that can be used in future projects."
        };
    }
}
