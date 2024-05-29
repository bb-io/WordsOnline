using System.IO.Compression;
using Blackbird.Applications.Sdk.Common.Files;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;

namespace Apps.WordsOnline.Utils;

public class ZipArchiveHelper(IFileManagementClient fileManagementClient)
{
    public async Task<byte[]> CreateZipFiles(List<FileReference> fileReferences)
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
    
    public async Task<List<FileReference>> ExtractZipFiles(byte[] zipFile)
    {
        await using var memoryStream = new MemoryStream(zipFile);
        memoryStream.Seek(0, SeekOrigin.Begin);
        
        var archive = new ZipArchive(memoryStream, ZipArchiveMode.Read, true);

        var fileReferences = new List<FileReference>();
        foreach (var entry in archive.Entries)
        {
            if(entry.Name.EndsWith(".zip"))
            {
                await using var zipStream = entry.Open();
                var zipBytes = await zipStream.GetByteData();
                var innerZipResponse = await ExtractZipFiles(zipBytes);
                
                fileReferences.AddRange(innerZipResponse);
                continue;
            }
            
            var fileReference = new FileReference
            {
                Name = entry.Name,
                ContentType = MimeTypes.GetMimeType(entry.Name)
            };
            
            await using var entryStream = entry.Open();
            var fileStream = new MemoryStream();
            await entryStream.CopyToAsync(fileStream);
            fileStream.Seek(0, SeekOrigin.Begin);
            
            fileReference = await fileManagementClient.UploadAsync(fileStream, fileReference.ContentType, fileReference.Name);
            fileReferences.Add(fileReference);
        }
        
        return fileReferences;
    }
}