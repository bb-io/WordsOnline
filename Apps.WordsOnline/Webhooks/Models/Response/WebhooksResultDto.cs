using Apps.WordsOnline.Api.Dtos;
using Newtonsoft.Json;

namespace Apps.WordsOnline.Webhooks.Models.Response;

public class WebhooksResultDto : ResponseBase
{
    [JsonProperty("result")]
    public ResultDto Result { get; set; } = new();
}

public class ResultDto
{
    [JsonProperty("list")]
    public List<WebhookDto> List { get; set; } = new();
    
    [JsonProperty("count")]
    public int Count { get; set; }
}