using Apps.WordsOnline.Api.Dtos;
using Blackbird.Applications.Sdk.Common;

namespace Apps.WordsOnline.Models.Responses;

public class RequestResponse
{
    public RequestResponse(SingleRequestDto dto)
    {
        RequestName = dto.RequestName;
        SourceLanguage = dto.SourceLanguageName;
        TargetLanguages = dto.TargetLanguages.Select(x => x.Code).ToList();
        ContentType = dto.ContentType;
        ServiceLevel = dto.ServiceLevel;
        IsAutoApproveQuote = dto.IsAutoApproveQuote;
        IsCompleted = dto.IsCompleted;
        IsSuccessed = dto.IsSuccessed;
        ErrorMessage = dto.ErrorMessage;
        CanConfirm = dto.CanConfirm;
        Status = dto.Status;
        State = dto.State;
        PmFee = dto.PmFee;
        IsHourlyDTP = dto.IsHourlyDTP;
        OrderId = dto.OrderId.ToString();
        OrderStatus = dto.OrderStatus;
        Project = dto.Project;
        ProjectGuid = dto.ProjectGuid;
        ProjectId = dto.ProjectId;
        ClientRequestId = dto.ClientRequestId;
        OrderDate = string.IsNullOrEmpty(dto.OrderDate) ? DateTime.MinValue : DateTime.Parse(dto.OrderDate);
        DueDate = string.IsNullOrEmpty(dto.DueDate) ? DateTime.MinValue : DateTime.Parse(dto.DueDate);
    }
    
    [Display("Request ID")]
    public string RequestId { get; set; } = string.Empty;
    
    [Display("Request name")] 
    public string RequestName { get; set; } = string.Empty;
    
    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target languages")]
    public List<string> TargetLanguages { get; set; } = new();
    
    [Display("Content type")]
    public string ContentType { get; set; } = string.Empty;
    
    [Display("Service level")]
    public string ServiceLevel { get; set; } = string.Empty;

    [Display("Is auto approve quote")]
    public bool IsAutoApproveQuote { get; set; }
    
    [Display("Is completed")]
    public bool IsCompleted { get; set; }
    
    [Display("Is successed")]
    public bool IsSuccessed { get; set; }
    
    [Display("Error message")]
    public string ErrorMessage { get; set; } = string.Empty;
    
    [Display("Can confirm")]
    public bool CanConfirm { get; set; }
    
    [Display("Status")]
    public string Status { get; set; } = string.Empty;
    
    [Display("State")]
    public string State { get; set; } = string.Empty;
    
    [Display("PM fee")]
    public double PmFee { get; set; }
    
    [Display("Is hourly DTP")]
    public bool IsHourlyDTP { get; set; }
    
    [Display("Order ID")]
    public string OrderId { get; set; } = string.Empty;
    
    [Display("Order status")]
    public string OrderStatus { get; set; } = string.Empty;
    
    [Display("Project")]
    public string Project { get; set; } = string.Empty;
    
    [Display("Project GUID")]
    public string ProjectGuid { get; set; } = string.Empty;
    
    [Display("Project ID")]
    public string ProjectId { get; set; } = string.Empty;
    
    [Display("Client request ID")]
    public string ClientRequestId { get; set; } = string.Empty;

    [Display("Order date")]
    public DateTime OrderDate { get; set; } = DateTime.MinValue;
    
    [Display("Due date")]
    public DateTime DueDate { get; set; } = DateTime.MinValue;
}