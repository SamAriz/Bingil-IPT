using System.Collections.Generic;

namespace BingilSamDashboard.Models
{
    public class ProfileInfo
    {
        public string FullName { get; set; } = "Sam Ariz G. Bingil";
        public string Title { get; set; } = "IT Student";
        public string Introduction { get; set; } = "I am an IT student with experience in a variety of programming languages and a track record of delivering high-quality code.";
        public List<string> Skills { get; set; } = new()
        {
            "Web Development",
            "Database Management",
            "SQL Database Management",
            "Linux/Unix Command line",
            "Python",
            "C#",
            "JAVA",
            "HTML",
            "CSS"
        };
        public List<string> Languages { get; set; } = new() { "English - Proficient", "Tagalog - Proficient" };
        public List<string> Hobbies { get; set; } = new() { "Eating", "Online Games", "Music", "Watching Movies", "Watching Anime", "Sleeping" };

        // Contact details
        public string Phone { get; set; } = "+63 9776652741";
        public string Email { get; set; } = "sam@gmail.com";
        public string Location { get; set; } = "Pob. 5, Midsayap, Cotabato";

        // Media links relative to wwwroot
        public List<string> ImagePaths { get; set; } = new() { "/img/profile-image.jpg", "/pic/pic-1.jpg", "/pic/pic-2.jpg" };
        public string AudioPath { get; set; } = "/msc/tiktok-msc.mp3";
        public string VideoPath { get; set; } = "/vid/tiktok-vid.mp4";
    }
}
