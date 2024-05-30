namespace Apps.WordsOnline.Api.Dtos;

public class RequestDto
{
    public int OrderId { get; set; }
    
    public string RequestGuid { get; set; } = string.Empty;
    
    public int RequestId { get; set; }
    
    public string RequestName { get; set; } = string.Empty;
    
    public string CreatedAt { get; set; } = string.Empty;
    
    public double Progress { get; set; }
    
    public string Status { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
    
    public string SourceLanguageName { get; set; } = string.Empty;
    
    public string SourceLanguageCode { get; set; } = string.Empty;
    
    public string TargetLanguageName { get; set; } = string.Empty;
    
    public string TargetLanguageCode { get; set; } = string.Empty;
    
    public string ServiceLevel { get; set; } = string.Empty;
    
    public string Token { get; set; } = string.Empty;
    
    public string ClientRequestId { get; set; } = string.Empty;
    
    public string ProjectGuid { get; set; } = string.Empty;
    
    public string ProjectId { get; set; } = string.Empty;
    
    public string DueDate { get; set; } = string.Empty;
}