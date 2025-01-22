using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
//using Newtonsoft.Json;

namespace PolygonAPITests.APITests.AlphaVantageAPI
{
    public class AlphaVantageLastDayOpenCloseData
    {
        [JsonPropertyName("01. symbol")]
        public string Symbol { get; set; }

        [JsonPropertyName("02. open")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Open { get; set; }

        [JsonPropertyName("03. high")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double High { get; set; }

        [JsonPropertyName("04. low")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Low { get; set; }

        [JsonPropertyName("05. price")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Price { get; set; }

        [JsonPropertyName("06. volume")]
        [JsonConverter(typeof(StringToLongConverter))]
        public long Volume { get; set; }

        [JsonPropertyName("07. latest trading day")]
        public string LastTradingDay { get; set; }

        [JsonPropertyName("08. previous close")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double PreviusClose { get; set; }

        [JsonPropertyName("09. change")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double Change { get; set; }

        [JsonPropertyName("10. change percent")]
        [JsonConverter(typeof(StringToDoubleConverter))]
        public double ChangePercent { get; set; }




    }
}
