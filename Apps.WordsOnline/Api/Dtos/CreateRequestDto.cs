using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class CreateRequestDto : ResponseBase
{
    [JsonProperty("result")]
    public string Result { get; set; } = string.Empty;
}