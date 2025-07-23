using System;
using System.Collections.Generic;

namespace ByteDev.Steam.Contract.Requests;

public class GetAppNewsRequest
{
    private IEnumerable<string> _feedNames;

    /// <summary>
    /// ID of app to get news for.
    /// </summary>
    public int AppId { get; init; }

    /// <summary>
    /// Maximum length for the content to return, if this is 0 the full content is returned,
    /// if it's less then a blurb is generated to fit.
    /// </summary>
    public int MaxLength { get; init; } = 0;

    /// <summary>
    /// Number of posts to retrieve. Default 20.
    /// </summary>
    public int Count { get; init; } = 20;

    /// <summary>
    /// Feed names to return news for.
    /// </summary>
    public IEnumerable<string> FeedNames
    {
        get => _feedNames ?? (_feedNames = new List<string>());
        init => _feedNames = value;
    }

    /// <summary>
    /// Retrieve posts earlier than this date.
    /// </summary>
    public DateTimeOffset? PostsBefore { get; init; } = null;
}