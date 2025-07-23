using System.Collections.Generic;
using System.Text;

namespace ByteDev.Steam.Infrastructure;

internal static class EnumerableToExtensions
{
    public static string ToDelimitedString<TSource>(this IEnumerable<TSource> source, string delimiter)
    {
        if (source == null)
            return string.Empty;

        var sb = new StringBuilder();

        foreach (var element in source)
        {
            sb.AppendIfNotEmpty(delimiter);
            sb.Append(element);
        }

        return sb.ToString();
    }

    public static string ToCsv<TSource>(this IEnumerable<TSource> source)
    {
        return ToDelimitedString(source, ",");
    }
}