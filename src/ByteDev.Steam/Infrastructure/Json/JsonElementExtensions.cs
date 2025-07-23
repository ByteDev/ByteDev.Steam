using System.Buffers;
using System.Text.Json;

namespace ByteDev.Steam.Infrastructure.Json;

public static class JsonElementExtensions
{
    public static T ToObject<T>(this JsonElement source, JsonSerializerOptions options = null)
    {
        var bufferWriter = new ArrayBufferWriter<byte>();

        using (var writer = new Utf8JsonWriter(bufferWriter))
            source.WriteTo(writer);

        return JsonSerializer.Deserialize<T>(bufferWriter.WrittenSpan, options);
    }
}