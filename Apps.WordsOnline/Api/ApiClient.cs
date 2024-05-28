using Apps.WordsOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.WordsOnline.Api;

public class ApiClient : RestClient
{
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? body,
        IEnumerable<AuthenticationCredentialsProvider> credentials)
    {
        var response = await Execute(endpoint, method, body, credentials.ToList());
        var result = JsonConvert.DeserializeObject<T>(response.Content!)!;
        
        return result;
    }

    public async Task<RestResponse> Execute(string endpoint, Method method, object? bodyObj,
        List<AuthenticationCredentialsProvider> creds)
    {
        var projectGuid = creds.Get(CredsNames.ProjectId).Value;
        endpoint = endpoint.Replace("[projectGuid]", projectGuid);
        
        var request = new ApiRequest(new()
        {
            Url = Urls.BaseUrl + endpoint,
            Method = method
        }, creds);

        return await ExecuteRequest(request);
    }
    
    private async Task<RestResponse> ExecuteRequest(ApiRequest request)
    {
        var response = await ExecuteAsync(request);
        if (!response.IsSuccessStatusCode)
            throw GetError(response);

        return response;
    }
    
    private static Exception GetError(RestResponse response)
    {
        return new($"Status code: {response.StatusCode}, Content: {response.Content}");
    }
}