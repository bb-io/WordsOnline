using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Webhooks.Handlers;
using Apps.WordsOnline.Webhooks.Models.Payloads;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.WordsOnline.Webhooks;

[WebhookList]
public class WebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Webhook("On request delivered", typeof(RequestDeliveredWebhookHandler), Description = "Triggered when request is delivered")]
    public Task<WebhookResponse<Payload>> ProjectChangedHandler(WebhookRequest webhookRequest)
    {
        var payload = JsonConvert.DeserializeObject<Payload>(webhookRequest.Body.ToString()!);
        return Task.FromResult(new WebhookResponse<Payload>
        {
            Result = payload
        });
    }
}