using RestSharp;
using System.IO.Compression;
using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Api.Models;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.WordsOnline.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Create request", Description = "Creates a new request based on the provided files")]
    public async Task<RequestResponse> CreateRequest([ActionParameter] CreateRequestRequest request)
    {
        try
        {
            var sources = request.SourceFiles.ToList();
            var references = request.ReferenceFiles?.ToList();

            var files = await UploadFiles(sources, references);
            var requestDto = new
            {
                requestName = request.RequestName,
                sourceLanguage = request.SourceLanguage,
                targetLanguages = request.TargetLanguages,
                contentTypeId = request.ContentType,
                serviceLevel = request.ServiceLevel,
                description = request.Description ?? "No description provided",
                fileList = files.Select(x => new
                {
                    guid = x.Guid,
                    name = x.Name,
                    type = x.Type
                }),
                clientRequestId = request.ClientRequestId ?? "Default ID",
                isAutoApprove = request.IsAutoApprove.HasValue ? request.IsAutoApprove.Value.ToString() : "True"
            };

            var response = await Client.ExecuteWithJson<BaseResponseDto<string>>("/requests", Method.Post, requestDto,
                Creds.ToList());

            var responseDto = await GetRequest(response.Result);
            return new RequestResponse(responseDto)
            {
                RequestId = response.Result
            };
        }
        catch (Exception e)
        {
            throw new Exception($"Got an error while creating a request. Exception: {e.Message}, " +
                                $"Exception type: {e.GetType()}, StackTrace: {e.StackTrace}, InnerException: {e.InnerException?.Message}");
        }
    }

    private async Task<SingleRequestDto> GetRequest(string requestId)
    {
        var request =
            await Client.ExecuteWithJson<BaseResponseDto<SingleRequestDto>>($"/requests/{requestId}", Method.Get, null,
                Creds.ToList());
        return request.Result;
    }

    private async Task<List<FileInfoDto>> UploadFiles(List<FileReference> sources, List<FileReference>? references)
    {
        var sourceZip = await CreateZipFiles(sources);
        byte[] fileBytes = await sourceZip.GetByteData();

        var contentType = MimeTypes.GetMimeType(".zip");
        var byteFiles = new List<ByteFile>
        {
            new()
            {
                KeyName = "Source",
                ContentType = contentType,
                FileName = "sources.zip",
                Bytes = fileBytes.ToList(),
            }
        };

        if (references != null)
        {
            var referenceZip = await CreateZipFiles(references);
            byte[] referenceBytes = await referenceZip.GetByteData();

            byteFiles.Add(new ByteFile
            {
                KeyName = "Reference",
                ContentType = contentType,
                FileName = "references.zip",
                Bytes = referenceBytes.ToList(),
            });
        }

        var files = await Client.ExecuteWithJson<BaseResponseDto<List<FileInfoDto>>>("/files", Method.Post, null,
            Creds.ToList().ToList(), byteFiles);
        return files.Result;
    }

    private async Task<MemoryStream> CreateZipFiles(List<FileReference> fileReferences)
    {
        var memoryStream = new MemoryStream();
        using var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        foreach (var fileReference in fileReferences)
        {
            var currentFile = archive.CreateEntry(fileReference.Name);
            await using var entryStream = currentFile.Open();

            var stream = await fileManagementClient.DownloadAsync(fileReference);
            await stream.CopyToAsync(entryStream);
        }

        memoryStream.Seek(0, SeekOrigin.Begin);
        return memoryStream;
    }

    public async Task<PaginationBaseResponseDto<RequestDto>> GetAllRequests()
    {
        var requests = await Client.ExecuteWithJson<PaginationBaseResponseDto<RequestDto>>(
            "/requests?$orderby=requestName",
            Method.Get, null, Creds.ToList());
        return requests;
    }
}