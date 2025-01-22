using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolygonAPITests.APITests.PolygonAPI
{
    public class Results
    {
        [JsonPropertyName("ticker")]
        public string Ticker { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("market")]
        public string Market { get; set; }

        [JsonPropertyName("locale")]
        public string Locale { get; set; }

        [JsonPropertyName("primary_exchange")]
        public string Primary_Exchange { get; set; }

        [JsonPropertyName("ntypeame")]
        public string Type { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("currency_name")]
        public string Currency_Name { get; set; }

        [JsonPropertyName("cik")]
        public string Cik { get; set; }

        [JsonPropertyName("composite_figi")]
        public string Composite_figi { get; set; }

        [JsonPropertyName("share_class_figi")]
        public string Share_class_figi { get; set; }

        [JsonPropertyName("market_cap")]
        public double Market_cap { get; set; }

        [JsonPropertyName("phone_number")]
        public string Phone_number { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("sic_code")]
        public string Sic_code { get; set; }

        [JsonPropertyName("sic_description")]
        public string Sic_description { get; set; }

        [JsonPropertyName("ticker_root")]
        public string Ticker_root { get; set; }

        [JsonPropertyName("homepage_url")]
        public string Homepage_url { get; set; }

        [JsonPropertyName("total_employees")]
        public int Total_employees { get; set; }

        [JsonPropertyName("list_date")]
        public string List_date { get; set; }

        [JsonPropertyName("branding")]
        public Branding Branding { get; set; }

        [JsonPropertyName("share_class_shares_outstanding")]
        public long Share_class_shares_outstanding { get; set; }

        [JsonPropertyName("weighted_shares_outstanding")]
        public long Weighted_shares_outstanding { get; set; }

        [JsonPropertyName("round_lot")]
        public int Round_lot { get; set; }
    }

    public class Address
    {
        public string address1 { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string postal_code { get; set; }
    }

    public class Branding
    {
        public string logo_url { get; set; }
        public string icon_url { get; set; }
    }
}
