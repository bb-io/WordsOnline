using Apps.WordsOnline.Api.Dtos;

namespace Apps.WordsOnline.Models.Responses;

public class UploadFileResponse
{
    public List<FileInfoDto> Files { get; set; } = new();
}