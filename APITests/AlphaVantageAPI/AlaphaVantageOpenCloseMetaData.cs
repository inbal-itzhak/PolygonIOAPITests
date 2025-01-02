//using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PolygonTests.APITests
{
    public class AlaphaVantageOpenCloseMetaData
    {
        [JsonPropertyName("1. Information")]
        public string Information { get; set; }

        [JsonPropertyName("2. Symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("3. Last Refreshed")]
        public string LastRefreshed { get; set; }

        [JsonPropertyName("4. Output Size")]
        public string OutputSize { get; set; }

        [JsonPropertyName("5. Time Zone")]
        public string TimeZone { get; set; }
    }
}
