namespace Apps.WordsOnline.Models.Responses;

public class GetRequestsResponse
{
    public List<RequestResponse> Requests { get; set; } = new ();
}