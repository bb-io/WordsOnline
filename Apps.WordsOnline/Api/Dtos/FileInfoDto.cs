using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class FileInfoDto
{
    [JsonProperty("guid"), Display("File GUID")]
    public string Guid { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;

    [JsonProperty("type")]
    public string Type { get; set; } = string.Empty;

    [JsonProperty("sourceLanguage")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [JsonProperty("targetLanguage")]
    public string TargetLanguage { get; set; } = string.Empty;
}