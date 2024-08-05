using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Webhooks.Handlers;
using Apps.WordsOnline.Webhooks.Models.Payloads;
using Apps.WordsOnline.Webhooks.Models.Requests;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Webhooks;
using Newtonsoft.Json;

namespace Apps.WordsOnline.Webhooks;

[WebhookList]
public class WebhookList(InvocationContext invocationContext) : AppInvocable(invocationContext)
{
    [Webhook("On request delivered", typeof(RequestDeliveredWebhookHandler), Description = "Triggered when request is delivered")]
    public Task<WebhookResponse<Payload>> OnRequestDelivered(WebhookRequest webhookRequest,
        [WebhookParameter] RequestDeliveredRequest request)
    {
        try
        {
            var payload = JsonConvert.DeserializeObject<Payload>(webhookRequest.Body.ToString()!)!;
            
            if(request.IncludeSourceFile.HasValue && request.IncludeSourceFile.Value)
            {
                payload.FileInfos = payload.FileInfos.Where(x => x.Type == "Source").ToList();
            }
            else
            {
                payload.FileInfos = payload.FileInfos.Where(x => x.Type == "Delivery").ToList();
            }
            
            if(request.TargetLanguages != null)
            {
                payload.TargetLanguages = payload.TargetLanguages.Where(x => request.TargetLanguages.Contains(x.TargetLanguageCode)).ToList();
                payload.FileInfos = payload.FileInfos.Where(x => request.TargetLanguages.Contains(x.TargetLanguageCode)).ToList();
            }
            
            return Task.FromResult(new WebhookResponse<Payload>
            {
                Result = payload
            });
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to deserialize payload: {webhookRequest.Body}, error: {e.Message}");
        }
    }
}