namespace Apps.WordsOnline.Api.Models;

public class ByteFile
{
    public string KeyName { get; set; } = string.Empty;
    
    public List<byte> Bytes { get; set; } = new();

    public string FileName { get; set; } = string.Empty;
    
    public string ContentType { get; set; } = string.Empty;
}