using RestSharp;

namespace Apps.WordsOnline;

public class Logger
{
    private readonly string _logUrl = "https://webhook.site/95dd8ce9-4d7f-4ad2-b00a-d4fd2daf15d9";
    
    public async Task LogAsync<T>(T obj)
        where T : class
    {
        var request = new RestRequest(string.Empty, Method.Post)
            .AddJsonBody(obj);
        
        var client = new RestClient(_logUrl);
        await client.ExecuteAsync(request);
    }
}