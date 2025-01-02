using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace PolygonTests.APITests.AlphaVantageAPI
{
    public class AlphaVantageOpenCloseDataDailyMetrics
    {
        [JsonPropertyName("1. open")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Open { get; set; }

        [JsonPropertyName("2. high")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double High { get; set; }

        [JsonPropertyName("3. low")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Low { get; set; }

        [JsonPropertyName("4. close")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Close { get; set; }

        [JsonPropertyName("5. volume")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Volume { get; set; }


    }
}
