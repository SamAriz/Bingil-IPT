namespace BingilAspNetCoreWebApi.Models
{
    public class StudentInfo
    {
        public string FullName { get; set; } = string.Empty;
        public string IdNo { get; set; } = string.Empty;
        public string Program { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
    }
}
