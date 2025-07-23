using ByteDev.Exceptions;
using ByteDev.Steam.Contract.Responses;
using ByteDev.Steam.Infrastructure;
using ByteDev.Steam.Infrastructure.Http;
using ByteDev.Steam.Infrastructure.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace ByteDev.Steam;

internal class ApiHttpResponseHandler
{
    public async Task<PlayerResponse> HandleGetUserAsync(
        HttpResponseMessage httpResponse,
        string steamId,
        CancellationToken cancellationToken)
    {
        await httpResponse.CheckStatusOkAsync();

        try
        {
            var json = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            using var jsonDoc = JsonDocument.Parse(json);

            var playersElement = jsonDoc.RootElement
                .GetProperty("response")
                .GetProperty("players");

            var players = playersElement.ToObject<IEnumerable<PlayerResponse>>();

            return players.SingleOrDefault(p => p.SteamId == steamId);
        }
        catch (Exception ex)
        {
            throw new ApiHttpResponseException("Unexpected response body from API.", ex);
        }
    }

    public async Task<T> HandleWithNewRootAsync<T>(
        HttpResponseMessage httpResponse, 
        string newRootElementName,
        CancellationToken cancellationToken)
    {
        await httpResponse.CheckStatusOkAsync();

        try
        {
            var json = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

            using var jsonDoc = JsonDocument.Parse(json);

            // Ignore the root and go straight to the single child containing element
            return jsonDoc.RootElement
                .GetProperty(newRootElementName)
                .ToObject<T>();
        }
        catch (Exception ex)
        {
            throw new ApiHttpResponseException("Unexpected response body from API.", ex);
        }
    }

    public async Task<T> HandleAsync<T>(HttpResponseMessage httpResponse, CancellationToken cancellationToken)
    {
        await httpResponse.CheckStatusOkAsync();

        try
        {
            return await ApiSerializer.DeserializeAsync<T>(httpResponse, cancellationToken);
        }
        catch (Exception ex)
        {
            throw new ApiHttpResponseException("Unexpected response body from API.", httpResponse.StatusCode, ex);
        }
    }
}