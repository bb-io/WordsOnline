using RestSharp;

namespace Apps.WordsOnline.Api;

public class ApiRequestParameters
{
    public string? Url { get; set; }
    
    public Method Method { get; init; }
}