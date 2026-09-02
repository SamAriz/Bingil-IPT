namespace BlazorApp2.Models
{
    public class StudentInfo
    {
        public string FullName { get; set; } = string.Empty;
        public string IdNo { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public string BirthDate { get; set; } = string.Empty;
        public int? Age { get; set; }
    }
}