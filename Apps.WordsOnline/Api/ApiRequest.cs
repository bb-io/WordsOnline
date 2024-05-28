using Apps.WordsOnline.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;
using RestSharp;

namespace Apps.WordsOnline.Api;

public class ApiRequest : RestRequest
{
    public ApiRequest(ApiRequestParameters requestParameters, IEnumerable<AuthenticationCredentialsProvider> creds)
        : base(requestParameters.Url, requestParameters.Method)
    {
        this.AddHeader("ApiKey", creds.Get(CredsNames.ApiKey).Value);
    }
}