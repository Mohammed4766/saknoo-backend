using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Configuration;

namespace Saknoo.Infrastructure.Services;


internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;

    public async Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
        var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);

        var blobClient = containerClient.GetBlobClient(fileName);

        await blobClient.UploadAsync(data);

        var blobUrl = blobClient.Uri.ToString();
        return blobUrl;
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl == null) return null;

        var blobName = GetBlobNameFromUrl(blobUrl);

        var sasBuilder = new BlobSasBuilder()
        {
            BlobContainerName = _blobStorageSettings.ContainerName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow.AddMonths(-5),
            ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(30),
            BlobName = blobName
        };


        sasBuilder.SetPermissions(BlobSasPermissions.Read);

        var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);


        var sasToken = sasBuilder
            .ToSasQueryParameters(new Azure.Storage.StorageSharedKeyCredential(blobServiceClient.AccountName, _blobStorageSettings.AccountKey))
            .ToString();

        return $"{blobUrl}?{sasToken}";
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        return uri.Segments.Last();
    }
}