using Newtonsoft.Json;

namespace Apps.WordsOnline.Webhooks.Models.Response;

public class WebhookDto
{
    [JsonProperty("guid")]
    public string Guid { get; set; } = string.Empty;
    
    [JsonProperty("createdBy")]
    public string CreatedBy { get; set; } = string.Empty;
    
    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; } = string.Empty;
    
    [JsonProperty("updatedBy")]
    public string UpdatedBy { get; set; } = string.Empty;
    
    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; set; } = string.Empty;
    
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("description")]
    public string Description { get; set; } = string.Empty;
    
    [JsonProperty("endpoint")] 
    public string Endpoint { get; set; } = string.Empty;
    
    [JsonProperty("subscriptions")]
    public string[] Subscriptions { get; set; } = Array.Empty<string>();
    
    [JsonProperty("authentication")]
    public object Authentication { get; set; } 
}