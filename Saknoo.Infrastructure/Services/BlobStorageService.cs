using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging; 
using Saknoo.Domain.Interfaces;
using Saknoo.Infrastructure.Configuration;

namespace Saknoo.Infrastructure.Services;

internal class BlobStorageService(IOptions<BlobStorageSettings> blobStorageSettingsOptions, ILogger<BlobStorageService> logger) : IBlobStorageService
{
    private readonly BlobStorageSettings _blobStorageSettings = blobStorageSettingsOptions.Value;
    private readonly ILogger<BlobStorageService> _logger = logger;

    public async Task<string> UploadToBlobAsync(Stream data, string fileName)
    {
        try
        {
            _logger.LogInformation("Starting upload for file {FileName}", fileName);

            var blobServiceClient = new BlobServiceClient(_blobStorageSettings.ConnectionString);
            var containerClient = blobServiceClient.GetBlobContainerClient(_blobStorageSettings.ContainerName);

            var blobClient = containerClient.GetBlobClient(fileName);

            await blobClient.UploadAsync(data);

            var blobUrl = blobClient.Uri.ToString();

            _logger.LogInformation("Successfully uploaded file {FileName} to {BlobUrl}", fileName, blobUrl);

            return blobUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file {FileName} to blob storage", fileName);
            throw;
        }
    }

    public string? GetBlobSasUrl(string? blobUrl)
    {
        if (blobUrl == null)
        {
            _logger.LogWarning("GetBlobSasUrl called with null blobUrl");
            return null;
        }

        try
        {
            var blobName = GetBlobNameFromUrl(blobUrl);

            _logger.LogInformation("Generating SAS URL for blob {BlobName}", blobName);

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

            var sasUrl = $"{blobUrl}?{sasToken}";

            _logger.LogInformation("SAS URL generated successfully for blob {BlobName}", blobName);

            return sasUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating SAS URL for blob {BlobUrl}", blobUrl);
            throw;
        }
    }

    private string GetBlobNameFromUrl(string blobUrl)
    {
        var uri = new Uri(blobUrl);
        var blobName = uri.Segments.Last();
        _logger.LogDebug("Extracted blob name {BlobName} from URL", blobName);
        return blobName;
    }
}
