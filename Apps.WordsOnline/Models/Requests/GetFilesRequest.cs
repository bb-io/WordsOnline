namespace Apps.WordsOnline.Models.Requests;

public class GetFilesRequest
{
    public IEnumerable<string>? Files { get; set; }
}