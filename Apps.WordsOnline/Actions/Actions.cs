using RestSharp;
using System.IO.Compression;
using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Api.Models;
using Apps.WordsOnline.Constants;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Models.Responses;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.WordsOnline.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    
    [Action("Create request", Description = "Creates a new request based on the provided files")]
    public async Task<RequestResponse> CreateRequest([ActionParameter] CreateRequestRequest request)
    {
        var sources = request.SourceFiles.ToList();
        var references = request.ReferenceFiles?.ToList();

        var files = await UploadFiles(sources, references);
        
        var project = await GetProject(Creds.Get(CredsNames.ProjectGuid).Value);
        var requestDto = new
        {
            requestName = request.RequestName,
            sourceLanguage = request.SourceLanguage,
            targetLanguages = request.TargetLanguages,
            contentTypeId = request.ContentType,
            serviceLevel = request.ServiceLevel,
            description = request.Description ?? "No description provided",
            projectId = project.ProjectId,
            fileList = files.Select(x => new
            {
                guid = x.Guid,
                name = x.Name,
                type = x.Type
            }),
            clientRequestId = request.ClientRequestId ?? "Default ID",
            isAutoApprove = request.IsAutoApprove.HasValue ? request.IsAutoApprove.Value.ToString() : "True"
        };

        var response = await Client.ExecuteWithJson<CreateRequestDto>("/requests", Method.Post, requestDto,
            Creds.ToList());

        var responseDto = await GetRequest(response.Result);
        
        return new RequestResponse(responseDto)
        {
            RequestId = response.Result
        };
    }
    
    public async Task<PaginationBaseResponseDto<RequestDto>> GetAllRequests()
    {
        var requests = await Client.ExecuteWithJson<PaginationBaseResponseDto<RequestDto>>(
            "/requests?$orderby=requestName",
            Method.Get, null, Creds.ToList());
        return requests;
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
        var contentType = MimeTypes.GetMimeType(".zip");
        var byteFiles = new List<ByteFile>
        {
            new()
            {
                KeyName = "Source",
                ContentType = contentType,
                FileName = "sources.zip",
                Bytes = sourceZip.ToList(),
            }
        };

        if (references != null)
        {
            var referenceZip = await CreateZipFiles(references);
            byteFiles.Add(new ByteFile
            {
                KeyName = "Reference",
                ContentType = contentType,
                FileName = "references.zip",
                Bytes = referenceZip.ToList(),
            });
        }

        var files = await Client.ExecuteWithJson<BaseResponseDto<List<FileInfoDto>>>("/files", Method.Post, null,
            Creds.ToList(), byteFiles);
        return files.Result;
    }

    private async Task<byte[]> CreateZipFiles(List<FileReference> fileReferences)
    {
        await using var memoryStream = new MemoryStream();
        var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true);

        foreach (var fileReference in fileReferences)
        {
            var currentFile = archive.CreateEntry(fileReference.Name);
            await using var entryStream = currentFile.Open();

            var stream = await fileManagementClient.DownloadAsync(fileReference);
            await stream.CopyToAsync(entryStream);
        }
        
        archive.Dispose();
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        return memoryStream.ToArray();
    }
    
    private async Task<ProjectDto> GetProject(string projectGuid)
    {
        var project = await Client.ExecuteWithJson<BaseResponseDto<ProjectDto>>($"/projects/{projectGuid}", Method.Get, null,
            Creds.ToList());
        return project.Result;
    }
}