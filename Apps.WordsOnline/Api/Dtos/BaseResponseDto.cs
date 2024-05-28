using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class ResponseBase
{
    [JsonProperty("status")]
    public int Status { get; set; } 

    [JsonProperty("code")]
    public string Code { get; set; } = string.Empty;
    
    [JsonProperty("message")]
    public string Message { get; set; } = string.Empty;
}

public class BaseResponseDto<T> : ResponseBase
    where T : class, new()
{
    [JsonProperty("result")]
    public T Result { get; set; } = new();
}


public class PaginationBaseResponseDto<T> : ResponseBase
{
    [JsonProperty("result")]
    public ResultDto<T> Result { get; set; } = new();
}

public class ResultDto<T>
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("list")]
    public List<T> List { get; set; } = new();
}