using Newtonsoft.Json;

namespace Apps.WordsOnline.Api.Dtos;

public class BasePaginatedDto<T>
{
    [JsonProperty("result")]
    public ResultDto<T> Result { get; set; }
    
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("code")]
    public string Code { get; set; }
    
    [JsonProperty("message")]
    public string Message { get; set; }
}

public class ResultDto<T>
{
    [JsonProperty("count")]
    public int Count { get; set; }

    [JsonProperty("list")]
    public List<T> List { get; set; }
}