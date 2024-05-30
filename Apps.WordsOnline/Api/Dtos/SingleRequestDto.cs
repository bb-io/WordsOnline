namespace Apps.WordsOnline.Api.Dtos;

public class SingleRequestDto
{
    public bool IsAutoApproveQuote { get; set; }
    
    public bool IsCompleted { get; set; }
    
    public bool IsSuccessed { get; set; }
    
    public string ErrorMessage { get; set; } = string.Empty;
    
    public bool CanConfirm { get; set; }
    
    public string Status { get; set; } = string.Empty;
    
    public string State { get; set; } = string.Empty;
    
    public double PmFee { get; set; }
    
    public bool IsHourlyDTP { get; set; }
    
    public int OrderId { get; set; }
    
    public string Project { get; set; } = string.Empty;
    
    public string RequestName { get; set; } = string.Empty;
    
    public int ItemsToTranslate { get; set; }
    
    public int RefrenceItems { get; set; }
    
    public string SourceLanguageName { get; set; } = string.Empty;
    
    public string SourceLanguageCode { get; set; } = string.Empty;
    
    public List<TargetLanguage> TargetLanguages { get; set; } = new();
    
    public string ContentType { get; set; } = string.Empty;
    
    public string ServiceLevel { get; set; } = string.Empty;
    
    public string EstimatedDays { get; set; } = string.Empty;
    
    public string OrderDate { get; set; } = string.Empty;
    
    public string DueDate { get; set; } = string.Empty;
    
    public string OrderStatus { get; set; } = string.Empty;
    
    public string RequestStatus { get; set; } = string.Empty;
    
    public string RequestState { get; set; } = string.Empty;
    
    public bool IsAutoApprove { get; set; }
    
    public string ClientRequestId { get; set; } = string.Empty;
    
    public string ProjectGuid { get; set; } = string.Empty;
    
    public string ProjectId { get; set; } = string.Empty;
    
    public bool ManualPreprocessing { get; set; }
    
    public string ProjectLabel { get; set; } = string.Empty;
    
    public bool DisplayTmSavings { get; set; }
}

public class TargetLanguage
{
    public string Code { get; set; } = string.Empty;
    
    public string Name { get; set; } = string.Empty;
}