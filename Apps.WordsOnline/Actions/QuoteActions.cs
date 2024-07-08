using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Identifiers;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.WordsOnline.Actions;

[ActionList]
public class QuoteActions(InvocationContext invocationContext)
    : AppInvocable(invocationContext)
{
    [Action("Get quote", Description = "Gets a quote based on the provided request ID")]
    public async Task<QuoteResponse> GetQuote([ActionParameter] RequestIdentifier request)
    {
        var endpoint = $"/Requests/{request.RequestId}/Quote";
        var response = await Client.ExecuteWithJson<BaseResponseDto<QuoteResponse>>(endpoint, Method.Get, null, Creds.ToList());
        return response.Result;
    }
    
    [Action("Approve quote", Description = "Approves a quote based on the provided request ID")]
    public async Task ApproveQuote([ActionParameter] ApproveQuoteRequest request)
    {
        var endpoint = $"/Requests/{request.RequestId}/Quote";

        var action = request.Action ?? "Approve";
        var body = new
        {
            action = action,
            description = request.Description ?? action
        };
        
        await Client.Execute(endpoint, Method.Put, body, Creds.ToList());
    }
}