namespace BingilAPI.Models
{
    public class IpLookupResult
    {
        // Support multiple IP lookup providers by including common fields returned
        public string? ip { get; set; }
        public string? query { get; set; } // older ip-api field
        public string? city { get; set; }
        public string? region { get; set; }
        public string? regionName { get; set; } // ip-api uses regionName
        public string? country { get; set; }
        public string? postal { get; set; } // ipinfo.io uses postal
        public string? zip { get; set; } // ip-api uses zip
        public string? loc { get; set; } // lat/lon combined as string from ipinfo
        public double? lat { get; set; }
        public double? lon { get; set; }
        public string? timezone { get; set; }
        public string? org { get; set; } // organization / ISP
        public string? isp { get; set; }
        public string? hostname { get; set; }
        public string? status { get; set; }
        public string? message { get; set; }
    }
}