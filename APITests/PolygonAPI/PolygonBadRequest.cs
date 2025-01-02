using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolygonTests.APITests.PolygonAPI
{
    public class PolygonBadRequest
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("request_id")]
        public string Request_Id { get; set; }

        [JsonPropertyName("error")]
        public string Error { get; set; }
    }
}
