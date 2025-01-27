using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolygonAPITests.APITests.AlphaVantageAPI
{
    public class GlobalQuoteData
    {
        [JsonPropertyName("Global Quote")]
        public AlphaVantageLastDayOpenCloseData GlobalQuote { get; set; }
    }
}
