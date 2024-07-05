namespace Apps.WordsOnline.Utils;

public static class QueryExtensions
{
    public static string FilterQueryString<T>(this T request)
    {
        var filter = string.Empty;
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {
            var value = property.GetValue(request)?.ToString();
            if (!string.IsNullOrEmpty(value))
            {
                if (!string.IsNullOrEmpty(filter))
                {
                    filter += " and ";
                }

                filter += $"{property.Name} eq '{value}'";
            }
        }

        return filter;
    }
}