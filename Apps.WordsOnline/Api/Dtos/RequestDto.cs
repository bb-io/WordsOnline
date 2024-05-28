namespace Apps.WordsOnline.Api.Dtos;

public class RequestDto
{
    public int OrderId { get; set; }
    
    public string RequestGuid { get; set; }
    
    public int RequestId { get; set; }
    
    public string RequestName { get; set; }
    
    public string CreatedAt { get; set; }
    
    public double Progress { get; set; }
    
    public string Status { get; set; }
    
    public string State { get; set; }
    
    public string SourceLanguageName { get; set; }
    
    public string SourceLanguageCode { get; set; }
    
    public string TargetLanguageName { get; set; }
    
    public string TargetLanguageCode { get; set; }
    
    public string ServiceLevel { get; set; }
    
    public string Token { get; set; }
    
    public string ClientRequestId { get; set; }
    
    public string ProjectGuid { get; set; }
    
    public string ProjectId { get; set; }
    
    public string DueDate { get; set; }
}