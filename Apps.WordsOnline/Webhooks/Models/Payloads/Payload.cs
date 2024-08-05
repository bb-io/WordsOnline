using Apps.WordsOnline.Api.Dtos;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.WordsOnline.Webhooks.Models.Payloads;

public class Payload
{
    [JsonProperty("requestGuid"), Display("Request GUID")]
    public string RequestGuid { get; set; } = string.Empty;
    
    [JsonProperty("requestName"), Display("Request name")]
    public string RequestName { get; set; } = string.Empty;
    
    [JsonProperty("progress"), Display("Progress"), DefinitionIgnore]
    public double Progress { get; set; }
    
    [JsonProperty("orderId"), Display("Order ID")]
    public int OrderId { get; set; }
    
    [JsonProperty("requestId"), Display("Request ID")]
    public int RequestId { get; set; }
    
    [JsonProperty("dueDate"), Display("Due date")]
    public DateTime DueDate { get; set; }
    
    [JsonProperty("createdAt"), Display("Created at")]
    public DateTime CreatedAt { get; set; }
    
    [JsonProperty("state"), Display("State")]
    public string State { get; set; } = string.Empty;
    
    [JsonProperty("status"), Display("Status")]
    public string Status { get; set; } = string.Empty;
    
    [JsonProperty("sourceLanguageName"), Display("Source language"), DefinitionIgnore]
    public string SourceLanguageName { get; set; } = string.Empty;
    
    [JsonProperty("sourceLanguageCode"), Display("Source language code")]
    public string SourceLanguageCode { get; set; }
    
    [JsonProperty("targetLanguages"), Display("Target languages")]
    public List<LanguagePairDto> TargetLanguages { get; set; } = new();
    
    [JsonProperty("serviceLevel"), Display("Service level")]
    public string ServiceLevel { get; set; } = string.Empty;
    
    [JsonProperty("token"), Display("Token"), DefinitionIgnore]
    public string Token { get; set; } = string.Empty;
    
    [JsonProperty("clientRequestId"), Display("Client request ID")]
    public string ClientRequestId { get; set; } = string.Empty;
    
    [JsonProperty("projectGuid"), Display("Project GUID")]
    public string ProjectGuid { get; set; } = string.Empty;
    
    [JsonProperty("projectId"), Display("Project ID")]
    public string ProjectId { get; set; } = string.Empty;
    
    [JsonProperty("fileInfos"), Display("File infos")]
    public List<FileInfoDto> FileInfos { get; set; } = new();
}

public class FileInfoDto
{
    [JsonProperty("guid"), Display("File GUID")]
    public string Guid { get; set; } = string.Empty;
    
    [JsonProperty("name"), Display("File name")]
    public string Name { get; set; } = string.Empty;
    
    [JsonProperty("type"), Display("File type")]
    public string Type { get; set; } = string.Empty;
    
    [JsonProperty("size"), Display("File size")]
    public int Size { get; set; }
    
    [JsonProperty("state"), Display("File state")]
    public string State { get; set; }
    
    [JsonProperty("sourceLanguageCode"), Display("Source language code")]
    public string SourceLanguageCode { get; set; } = string.Empty;
    
    [JsonProperty("targetLanguageCode"), Display("Target language code")]
    public string TargetLanguageCode { get; set; } = string.Empty;
}