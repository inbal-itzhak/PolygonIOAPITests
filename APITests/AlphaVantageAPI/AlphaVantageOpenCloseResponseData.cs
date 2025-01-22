using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace PolygonAPITests.APITests.AlphaVantageAPI
{
    public class AlphaVantageOpenCloseResponseData
    {
        [JsonPropertyName("Meta Data")]
        public AlaphaVantageOpenCloseMetaData MetaData { get; set; }

        [JsonPropertyName("Time Series (Daily)")]
        public Dictionary<string, AlphaVantageOpenCloseDataDailyMetrics> TimeSeriesDaily { get; set; }
    }
}
