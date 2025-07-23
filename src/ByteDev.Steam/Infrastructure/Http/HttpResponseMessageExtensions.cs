using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ByteDev.Exceptions;

namespace ByteDev.Steam.Infrastructure.Http;

internal static class HttpResponseMessageExtensions
{
    public static async Task CheckStatusOkAsync(this HttpResponseMessage source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        if (source.StatusCode == HttpStatusCode.Forbidden)
            throw new ApiHttpResponseException("API returned forbidden. Please check your API key is valid. If key is valid then check other parameters are valid.", source.StatusCode);

        if (source.StatusCode != HttpStatusCode.OK)
        {
            var content = await source.Content.ReadAsStringAsync();

            throw new ApiHttpResponseException("API returned unexpected status.", source.StatusCode, content);
        }
    }
}