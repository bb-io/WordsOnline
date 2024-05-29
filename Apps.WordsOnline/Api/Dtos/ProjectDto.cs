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

    [JsonProperty("contentTypes")]
    public List<string> ContentTypes { get; set; } = new();
}