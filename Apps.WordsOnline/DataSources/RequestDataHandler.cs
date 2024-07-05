using Apps.WordsOnline.Api;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;

namespace Apps.WordsOnline.DataSources;

public class RequestDataHandler(InvocationContext invocationContext) : AppInvocable(invocationContext), IAsyncDataSourceHandler
{
    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
    {
        var client = new ApiClient();
        var requests = await client.PaginateRequests(new SearchRequestsRequest(), InvocationContext.AuthenticationCredentialsProviders.ToList());

        return requests.Result.List
            .Where(x => string.IsNullOrEmpty(context.SearchString) || x.RequestName.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(x => x.RequestGuid, x => x.RequestName);
    }
}