using Apps.WordsOnline.Api;
using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Webhooks.Models.Response;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Webhooks;
using RestSharp;

namespace Apps.WordsOnline.Webhooks.Handlers;

public class BaseWebhookHandler(string @event) : IWebhookEventHandler
{
    private string @event = @event;

    public async Task SubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var body = new
        {
            name = Guid.NewGuid().ToString(),
            endpoint = values["payloadUrl"],
            description = "bb.io webhook test",
            subscriptions = new[] { @event }
        };

        var client = new ApiClient();
        await client.ExecuteWithJson<ResponseBase>("/WebHooks", Method.Post, body, authenticationCredentialsProvider);
    }

    public async Task UnsubscribeAsync(IEnumerable<AuthenticationCredentialsProvider> authenticationCredentialsProvider, Dictionary<string, string> values)
    {
        var client = new ApiClient();
        var authenticationCredentialsProviders = authenticationCredentialsProvider as AuthenticationCredentialsProvider[] ?? authenticationCredentialsProvider.ToArray();
       
        var webhooks = await client.ExecuteWithJson<WebhooksResultDto>("/WebHooks", Method.Get, null, authenticationCredentialsProviders);
        var webhook = webhooks.Result?.List.FirstOrDefault(x => x.Endpoint == values["payloadUrl"] && x.Subscriptions.Contains(@event));
        if (webhook != null)
        {
            await client.ExecuteWithJson<ResponseBase>($"/WebHooks/{webhook.Guid}", Method.Delete, null, authenticationCredentialsProviders);
        }
    }
}