using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PolygonTests.APITests
{
    public class StringToLongConverter : JsonConverter<long>
    {
        public override long Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String &&
                long.TryParse(reader.GetString(), out var result))
            {
                return result;
            }
            return 0L; // Default value if conversion fails
        }

        public override void Write(Utf8JsonWriter writer, long value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
