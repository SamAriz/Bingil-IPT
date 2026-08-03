namespace BingilAPI.Models
{
    public class IpLookupResult
    {
        public string status { get; set; } = "";
        public string message { get; set; } = "";
        public string country { get; set; } = "";
        public string regionName { get; set; } = "";
        public string city { get; set; } = "";
        public string zip { get; set; } = "";
        public double lat { get; set; }
        public double lon { get; set; }
        public string timezone { get; set; } = "";
        public string isp { get; set; } = "";
        public string org { get; set; } = "";
        public string query { get; set; } = "";
    }
}
