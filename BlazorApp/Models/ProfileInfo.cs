namespace BlazorApp1.Models
{
    // Returned by GET /api/profile  (sidebar information)
    public class ProfileInfo
    {
        public string Name { get; set; } = "";
        public string Role { get; set; } = "";
        public ContactInfo Contact { get; set; } = new();
        public List<string> Skills { get; set; } = new();
        public List<string> Languages { get; set; } = new();
        public List<string> Hobbies { get; set; } = new();
        public List<string> Weaknesses { get; set; } = new();
        public List<string> Experiences { get; set; } = new();
        public List<string> Descriptions { get; set; } = new();
    }

    // Returned by GET /api/details  (its own API, loaded with a button)
    public class DetailsInfo
    {
        public int? Age { get; set; }
        public string StudentId { get; set; } = "";
        public string School { get; set; } = "";
        public string Course { get; set; } = "";
        public string Birthday { get; set; } = "";
    }

    public class ContactInfo
    {
        public string Phone { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
    }
}
