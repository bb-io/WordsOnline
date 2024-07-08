using RestSharp;
using Apps.WordsOnline.Api.Dtos;
using Apps.WordsOnline.Api.Models;
using Apps.WordsOnline.Constants;
using Apps.WordsOnline.Invocables;
using Apps.WordsOnline.Models.Identifiers;
using Apps.WordsOnline.Models.Requests;
using Apps.WordsOnline.Models.Responses;
using Apps.WordsOnline.Utils;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using Blackbird.Applications.Sdk.Utils.Extensions.Sdk;

namespace Apps.WordsOnline.Actions;

[ActionList]
public class Actions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : AppInvocable(invocationContext)
{
    [Action("Search requests", Description = "Searches for requests based on the provided filters")]
    public async Task<GetRequestsResponse> SearchRequests([ActionParameter] SearchRequestsRequest request)
    {
        var requests = await Client.PaginateRequests(request, Creds.ToList());
        return new GetRequestsResponse
        {
            Requests = requests.Result.List.Select(x => new RequestResponse(x)).ToList()
        };
    }
    
    [Action("Get request", Description = "Gets a request based on the provided ID")]
    public async Task<RequestResponse> GetRequest([ActionParameter] RequestIdentifier request)
    {
        var requestDto = await GetRequest(request.RequestId);
        return new RequestResponse(requestDto) { RequestGuid = request.RequestId };
    }
    
    [Action("Create request", Description = "Creates a new request based on the provided files")]
    public async Task<RequestResponse> CreateRequest([ActionParameter] CreateRequestRequest request)
    {
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

    [Action("Upload file", Description = "Uploads file to the request based on provided Requst GUID")]
    public async Task<UploadFileResponse> UploadFile([ActionParameter] UploadFileRequest request)
    {
        var files = await UploadFiles(request.RequestId, request.File, request.ReferenceFiles?.ToList());
        return new UploadFileResponse
        {
            Files = files.Select(x => new FileInfoDto
            {
                Guid = x.Guid,
                Name = x.Name,
                Type = x.Type
            }).ToList()
        };
    }
    
    [Action("Submit request", Description = "Submits the request based on the provided ID")]
    public async Task SubmitRequest([ActionParameter] SubmitRequestRequest request)
    {
        var notifyFileUploaded = request.NotifyFileUploaded ?? true;
        var endpoint = $"/Requests?notifyFileUploaded={notifyFileUploaded}";
        var body = new
        {
            requestGuid = request.RequestId,
        };

        await Client.Execute(endpoint, Method.Put, body, Creds.ToList());
    }
    
    [Action("Update request status", Description = "Updates the status of a request based on the provided ID")]
    public async Task UpdateRequestStatus([ActionParameter] UpdateRequestStatusRequest request)
    {
        var endpoint = $"/Requests/{request.RequestId}/Actions";
        var body = new
        {
            action = request.Action,
            description = request.Description ?? "No description provided",
        };

        await Client.Execute(endpoint, Method.Post, body, Creds.ToList());
    }

    [Action("Download files", Description = "Downloads the files from the request")]
    public async Task<DownloadFilesResponse> DownloadFiles([ActionParameter] DownloadFilesRequest request)
    {            
        var zipArchive = new ZipArchiveHelper(fileManagementClient);

        if (request.DownloadAllFiles.HasValue && request.DownloadAllFiles.Value)
        {
            var allFiles = await Client.Execute($"/requests/{request.RequestId}/files/download", Method.Get, null,
                Creds.ToList());
            var bytes = allFiles.RawBytes!;
            var fileReferences = await zipArchive.ExtractZipFiles(bytes);

            return new DownloadFilesResponse
            {
                Files = fileReferences
            };
        }

        if (request.Files != null && request.Files.Any())
        {
            var fileReferences = new List<FileReference>();
            
            foreach (var file in request.Files)
            {
                var specificFiles = await Client.Execute($"/requests/{request.RequestId}/files/{file}/download",
                    Method.Get, null, Creds.ToList());
                var bytes = specificFiles.RawBytes!;
                
                var unzippedFiles = await zipArchive.ExtractZipFiles(bytes);
                fileReferences.AddRange(unzippedFiles);
            }
            
            return new DownloadFilesResponse
            {
                Files = fileReferences
            };
        }
        
        var allFilesMetadata = await GetFilesFromRequest(request.RequestId);
        var deliveredFilesMetadata = allFilesMetadata.Where(f => f.Type.Equals("Delivery", StringComparison.OrdinalIgnoreCase)).ToList();
            
        var deliveredFiles = new List<FileReference>();
        foreach (var fileMetadata in deliveredFilesMetadata)
        {
            var specificFile = await Client.Execute($"/requests/{request.RequestId}/files/{fileMetadata.Guid}/download",
                Method.Get, null, Creds.ToList());
            var bytes = specificFile.RawBytes;

            if (bytes != null)
            {
                var unzippedFiles = await zipArchive.ExtractZipFiles(bytes);
                deliveredFiles.AddRange(unzippedFiles);
            }
        }

        return new DownloadFilesResponse
        {
            Files = deliveredFiles
        };
    }
    
    public async Task<ProjectDto> GetProject(string projectGuid)
    {
        var project = await Client.ExecuteWithJson<BaseResponseDto<ProjectDto>>($"/projects/{projectGuid}", Method.Get,
            null,
            Creds.ToList());
        return project.Result;
    }

    public async Task<List<FileInfoDto>> GetFilesFromRequest(string requestId)
    {
        var files = await Client.ExecuteWithJson<BaseResponseDto<List<FileInfoDto>>>(
            $"/requests/{requestId}/files",
            Method.Get, null, Creds.ToList());
        return files.Result;
    }

    private async Task<SingleRequestDto> GetRequest(string requestId)
    {
        var request =
            await Client.ExecuteWithJson<BaseResponseDto<SingleRequestDto>>($"/requests/{requestId}", Method.Get, null,
                Creds.ToList());
        return request.Result;
    }

    private async Task<List<FileInfoDto>> UploadFiles(string requestGuid, FileReference source, List<FileReference>? references)
    {
        var sourceFile = await fileManagementClient.DownloadAsync(source);
        var bytes = await sourceFile.GetByteData();
        var byteFiles = new List<ByteFile>
        {
            new()
            {
                KeyName = "Source",
                ContentType = source.ContentType,
                FileName = source.Name,
                Bytes = bytes.ToList(),
            }
        };

        if (references != null)
        {
            var zipArchive = new ZipArchiveHelper(fileManagementClient);
            var referenceZip = await zipArchive.CreateZipFiles(references);
            byteFiles.Add(new ByteFile
            {
                KeyName = "Reference",
                ContentType = "application/zip",
                FileName = "references.zip",
                Bytes = referenceZip.ToList(),
            });
        }

        var files = await Client.ExecuteWithFormData<BaseResponseDto<List<FileInfoDto>>>("/files", 
            new Dictionary<string, string>
            {
                { "RequestGuid", requestGuid }
            }, 
            byteFiles,
            Method.Post,
            Creds.ToList());
        
        return files.Result;
    }
}