using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class FileInfoDto
{
    [JsonProperty("guid")]
    public string Guid { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;
}