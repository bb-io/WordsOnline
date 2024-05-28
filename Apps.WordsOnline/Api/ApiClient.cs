using Apps.WordsOnline.Api.Models;
using Apps.WordsOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.WordsOnline.Api;

public class ApiClient : RestClient
{
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? body,
        IEnumerable<AuthenticationCredentialsProvider> credentials, List<ByteFile>? files = null)
    {
        var response = await Execute(endpoint, method, body, credentials.ToList(), files);
        var result = JsonConvert.DeserializeObject<T>(response.Content!)!;
        
        return result;
    }

    public async Task<RestResponse> Execute(string endpoint, Method method, object? bodyObj,
        List<AuthenticationCredentialsProvider> creds, List<ByteFile>? files = null)
    {
        var projectGuid = creds.Get(CredsNames.ProjectId).Value;
        endpoint = endpoint.Replace("[projectGuid]", projectGuid);
        
        RestRequest request = new ApiRequest(new()
        {
            Url = Urls.BaseUrl + endpoint,
            Method = method
        }, creds);

        if (files != null)
        {
            foreach (var file in files)
            {
                request = request.AddFile(file.KeyName, file.Bytes.ToArray(), file.FileName, file.ContentType);
            }
        }

        if (bodyObj != null)
        {
            request.AddJsonBody(bodyObj);
        }

        return await ExecuteRequest(request);
    }
    
    private async Task<RestResponse> ExecuteRequest(RestRequest request)
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