public interface IStorageService
{
    Task UploadFileAsync(string filePath, string blobName);
    Task<Stream> DownloadFileAsync(string blobName);
    Task UploadPdfAsync(byte[] pdfBytes, string blobName);
    string GenerateBlobSasToken(string blobName);
}