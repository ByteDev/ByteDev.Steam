using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.Steam.Infrastructure;

internal static class ApiSerializer
{
    public static async Task<T> DeserializeAsync<T>(HttpResponseMessage httpResponse, CancellationToken cancellationToken = default)
    {
        var options = CreateJsonSerializerOptions();

        await using var contentStream = await httpResponse.Content.ReadAsStreamAsync(cancellationToken);

        return await JsonSerializer.DeserializeAsync<T>(contentStream, options, cancellationToken);
    }

    private static JsonSerializerOptions CreateJsonSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = false
        };
    }
}