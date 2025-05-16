using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using SiteApi.Services;
using System;
using System.IO;
using System.Threading.Tasks;

public class ImageUploadService : IImageUploadService
{
    private readonly string _bucketName;
    private readonly StorageClient _storageClient;

    public ImageUploadService(IConfiguration configuration)
    {
        _bucketName = configuration["GoogleCloud:BucketName"];
        var credentialPath = configuration["GoogleCloud:CredentialPath"];
        var credential = GoogleCredential.FromFile(credentialPath);
        _storageClient = StorageClient.Create(credential);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Arquivo inválido.");

        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var objectName = Guid.NewGuid() + Path.GetExtension(file.FileName);

        await _storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, memoryStream);

        return $"https://storage.googleapis.com/{_bucketName}/{objectName}";
    }
}