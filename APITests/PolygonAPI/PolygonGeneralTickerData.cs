using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolygonAPITests.APITests.PolygonAPI
{
    public class PolygonGeneralTickerData
    {
            public string request_id { get; set; }

            [JsonPropertyName("results")]
            public Results results { get; set; }
            public string status { get; set; }
        
    }
}
