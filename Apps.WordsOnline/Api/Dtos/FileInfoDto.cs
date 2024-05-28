using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class FileInfoDto
{
    [JsonProperty("guid")]
    public string Guid { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }
}