using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class ProjectDto
{
    [JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    
    [JsonProperty("text")]
    public string Text { get; set; } = string.Empty;

    [JsonProperty("projectId")]
    public string ProjectId { get; set; } = string.Empty;
    
    [JsonProperty("languagePairs")]
    public List<LanguagePairDto> LanguagePairs { get; set; } = new();

    [JsonProperty("contentTypes")]
    public List<string> ContentTypes { get; set; } = new();
    
    [JsonProperty("serviceLevels")]
    public List<ServiceLevelDto> ServiceLevels { get; set; } = new();
}

public class LanguagePairDto
{
    [JsonProperty("sourceLanguageCode")]
    public string SourceLanguageCode { get; set; } = string.Empty;
    
    [JsonProperty("sourceLanguageName")]
    public string SourceLanguageName { get; set; } = string.Empty;
    
    [JsonProperty("targetLanguageCode")]
    public string TargetLanguageCode { get; set; } = string.Empty;
    
    [JsonProperty("targetLanguageName")]
    public string TargetLanguageName { get; set; } = string.Empty;
}

public class ServiceLevelDto
{
    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("contentType")]
    public string ContentType { get; set; } = string.Empty;
}