using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Api.Models;
using Apps.WordsOnline.Constants;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Utils;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Blackbird.Applications.Sdk.Utils.Extensions.Http;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using RestSharp;

namespace Apps.WordsOnline.Api;

public class ApiClient : RestClient
{
    public async Task<T> ExecuteWithJson<T>(string endpoint, Method method, object? body,
        IEnumerable<AuthenticationCredentialsProvider> credentials, List<ByteFile>? files = null)
        where T : ResponseBase
    {
        var response = await Execute(endpoint, method, body, credentials.ToList(), files);
        var result = JsonConvert.DeserializeObject<T>(response.Content!)!;
        if (result.Status == -1)
        {
            throw new PluginApplicationException($"Status code: {result.Status}, Code: {result.Code}, Message: {result.Message}");
        }
        
        return result;
    }
    
    public async Task<TResponse> ExecuteWithFormData<TResponse>(string endpoint, 
        Dictionary<string, string> properties,
        List<ByteFile>? files,
        Method method,
        IEnumerable<AuthenticationCredentialsProvider> credentials)
        where TResponse : ResponseBase
    {
        RestRequest request = new ApiRequest(new()
        {
            Url = Urls.BaseUrl + endpoint,
            Method = method
        }, credentials.ToList());

        foreach (var (key, value) in properties)
        {
            request.AddParameter(key, value);
        }
        
        if (files != null)
        {
            foreach (var file in files)
            {
                request = request.AddFile(file.KeyName, file.Bytes.ToArray(), file.FileName, file.ContentType);
            }
        }

        var response = await ExecuteRequest(request);
        var result = JsonConvert.DeserializeObject<TResponse>(response.Content!)!;
        if (result.Status == -1)
        {
            throw new PluginApplicationException($"Status code: {result.Status}, Code: {result.Code}, Message: {result.Message}");
        }
        
        return result;
    }
    
    public async Task Execute(string endpoint, Method method, object? body,
        IEnumerable<AuthenticationCredentialsProvider> credentials, List<ByteFile>? files = null)
    {
        var response = await Execute(endpoint, method, body, credentials.ToList(), files);
        var result = JsonConvert.DeserializeObject<ResponseBase>(response.Content!)!;
        if (result.Status == -1)
        {
            throw new PluginApplicationException($"Status code: {result.Status}, Code: {result.Code}, Message: {result.Message}");
        }
    }

    public async Task<RestResponse> Execute(string endpoint, Method method, object? bodyObj,
        List<AuthenticationCredentialsProvider> creds, List<ByteFile>? files = null)
    {
        var projectGuid = creds.Get(CredsNames.ProjectGuid).Value;
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
            request.WithJsonBody(bodyObj, new()
            {
                ContractResolver = new DefaultContractResolver()
                {
                    NamingStrategy = new CamelCaseNamingStrategy()
                },
                NullValueHandling = NullValueHandling.Ignore
            });        
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
        return new PluginApplicationException($"Status code: {response.StatusCode}, Content: {response.Content}");
    }
    
    public async Task<PaginationBaseResponseDto<RequestDto>> PaginateRequests(SearchRequestsRequest request, 
        List<AuthenticationCredentialsProvider> creds)
    {
        const int pageSize = 100;
        var allRequests = new List<RequestDto>();
        
        var skip = 0;
        var moreData = true;
        while (moreData)
        {
            var paginatedResult = await FetchRequestsWithPagination(skip, pageSize, request, creds);
            if (paginatedResult?.Result?.List != null && paginatedResult.Result.List.Any())
            {
                allRequests.AddRange(paginatedResult.Result.List);
                skip += pageSize;
            }
            else
            {
                moreData = false;
            }
        }

        return new PaginationBaseResponseDto<RequestDto>
        {
            Result = new ResultDto<RequestDto>
            {
                Count = allRequests.Count,
                List = allRequests
            },
            Status = 1,
            Code = "0000",
            Message = "Success"
        };
    }
    
    private async Task<PaginationBaseResponseDto<RequestDto>> FetchRequestsWithPagination(int skip, int top, SearchRequestsRequest request,
        List<AuthenticationCredentialsProvider> creds)
    {
        var endpoint = $"/requests?$orderby=requestName&$skip={skip}&$top={top}";
        var filter = request.FilterQueryString();

        if (!string.IsNullOrEmpty(filter))
        {
            endpoint += $"&$filter={filter}";
        }
        
        var requests = await ExecuteWithJson<PaginationBaseResponseDto<RequestDto>>(endpoint, Method.Get, null, creds);
        return requests;
    }
}