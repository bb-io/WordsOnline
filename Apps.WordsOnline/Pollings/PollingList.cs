using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Models.Responses;
using Apps.WordsOnline.Pollings.Models;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;

namespace Apps.WordsOnline.Pollings;

[PollingEventList]
public class PollingList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [PollingEvent("On requests delivered", Description = "Triggered when a requests is delivered")]
    public async Task<PollingEventResponse<Memory, GetRequestsResponse>>
        OnBlogPostsCreatedOrUpdated(PollingEventRequest<Memory> request)
    {
        var deliveredRequests = await Client.PaginateRequests(new SearchRequestsRequest()
        {
            Status = "Delivered"
        }, Creds.ToList());
        
        if(request.Memory is null)
        {
            return new PollingEventResponse<Memory, GetRequestsResponse>
            {
                FlyBird = false,
                Memory = new Memory { DeliveredRequestGuids = deliveredRequests.Result.List.Select(x => x.RequestGuid).ToList() },
                Result = null
            };
        }
        
        var newDeliveredRequests = deliveredRequests.Result.List
            .Where(x => !request.Memory.DeliveredRequestGuids.Contains(x.RequestGuid))
            .ToList();

        if (newDeliveredRequests.Count == 0)
        {
            return new PollingEventResponse<Memory, GetRequestsResponse>
            {
                FlyBird = false,
                Memory = new Memory { DeliveredRequestGuids = deliveredRequests.Result.List.Select(x => x.RequestGuid).ToList() },
                Result = null
            };
        }
        
        return new PollingEventResponse<Memory, GetRequestsResponse>
        {
            FlyBird = true,
            Memory = new Memory { DeliveredRequestGuids = deliveredRequests.Result.List.Select(x => x.RequestGuid).ToList() },
            Result = new GetRequestsResponse
            {
                Requests = newDeliveredRequests.Select(x => new RequestResponse(x)).ToList()
            }
        };
    }
}