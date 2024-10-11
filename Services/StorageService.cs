using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas;

public class StorageService : IStorageService
{
    private readonly string connectionString;
    private readonly string containerName;
    private StorageSharedKeyCredential credentials;

    public StorageService(string connectionString, string containerName)
    {
        this.connectionString = connectionString;
        this.containerName = containerName;
    }

    public async Task UploadFileAsync(string filePath, string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        // Get a reference to a blob
        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        // Upload the file
        using (FileStream fs = File.OpenRead(filePath))
        {
            await blobClient.UploadAsync(fs, true);
            fs.Close();
        }
    }

    public async Task<Stream> DownloadFileAsync(string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        if (await blobClient.ExistsAsync())
        {
            BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();
            return blobDownloadInfo.Content;
        }

        return null;
    }

    public async Task UploadPdfAsync(byte[] pdfBytes, string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        using (MemoryStream memoryStream = new MemoryStream(pdfBytes))
        {
            await blobClient.UploadAsync(memoryStream, true);
        }
    }

    public string GenerateBlobSasToken(string blobName)
    {
        BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);
        BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

        BlobClient blobClient = containerClient.GetBlobClient(blobName);

        var permissions = new BlobSasBuilder
        {
            BlobContainerName = containerName,
            BlobName = blobName,
            Resource = "b",
            StartsOn = DateTimeOffset.UtcNow.AddMinutes(-5),
            ExpiresOn = DateTimeOffset.UtcNow.AddHours(1),
            Protocol = SasProtocol.Https
        }.ToSasQueryParameters(credentials).ToString();

        return $"{blobClient.Uri}?{permissions}";
    }

}