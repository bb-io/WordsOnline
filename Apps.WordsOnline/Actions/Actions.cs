using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Invocables;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.WordsOnline.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext)
    : AppInvocable(invocationContext)
{
    public async Task<BasePaginatedDto<RequestDto>> GetAllRequests()
    {
        var requests = await Client.ExecuteWithJson<BasePaginatedDto<RequestDto>>("/requests?$orderby=requestName", Method.Get, null, Creds.ToList());
        return requests;
    }
}