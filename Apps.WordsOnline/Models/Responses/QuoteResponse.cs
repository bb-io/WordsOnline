using Blackbird.Applications.Sdk.Common;

namespace Apps.WordsOnline.Models.Responses;

public class QuoteResponse
{
    [Display("Word count")]
    public double WordCount { get; set; }
    
    [Display("DTP page count")]
    public double DtpPageCount { get; set; }
    
    [Display("Display TM savings")]
    public bool DisplayTmSavings { get; set; }
    
    [Display("Amount without TM")]
    public double AmountWithoutTM { get; set; } 
    
    [Display("TM leverage")]
    public double TmLeverage { get; set; }
    
    [Display("TM leverage percentage")]
    public double TmLeveragePercentage { get; set; }
    
    [Display("Quote sections")]
    public List<QuoteSectionDto> QuoteSections { get; set; } = new();
    
    [Display("Quote items")]
    public List<QuoteItemDto> QuoteItems { get; set; } = new();
}

public class QuoteSectionDto
{
    [Display("Name")]
    public string Name { get; set; } = string.Empty;
    
    [Display("Amount")]
    public double Amount { get; set; }
    
    [Display("Currency")]
    public string Currency { get; set; } = string.Empty;
}

public class QuoteItemDto
{
    public string Service { get; set; } = string.Empty;
    
    [Display("Source language")]
    public string SourceLanguage { get; set; } = string.Empty;
    
    [Display("Target language")]
    public string TargetLanguage { get; set; } = string.Empty;
    
    public double Quantity { get; set; }
    
    public double Amount { get; set; }
    
    public string Currency { get; set; } = string.Empty;
    
    [Display("Unit code")]
    public string UnitCode { get; set; } = string.Empty;
    
    [Display("Unit rate")]
    public double UnitRate { get; set; }
    
    public string Type { get; set; } = string.Empty;
}
