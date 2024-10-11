using System.Text;
using ServiceCollectionAPI.Services.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf.Canvas.Parser.Listener;
using iText.Layout;
using iText.Layout.Element;

public class PdfFileService : IPdfFileService
{
    private readonly IStorageService storageService;

    public PdfFileService(IStorageService storageService)
    {
        this.storageService = storageService;
    }

    public async Task<string> GeneratePdfAsync(string templatePath, Dictionary<string, string> templateData, string blobName)
    {
        // Load the existing PDF template
        var pdfTemplateBytes = File.ReadAllBytes(templatePath);

        // Replace placeholders with actual data
        var modifiedPdfBytes = ReplaceTemplateVariables(pdfTemplateBytes, templateData);

        // Save the modified PDF to Azure Blob Storage
        await storageService.UploadPdfAsync(modifiedPdfBytes, blobName);

        // Generate a SAS token for the stored PDF
        return storageService.GenerateBlobSasToken(blobName);
    }

    private byte[] ReplaceTemplateVariables(byte[] pdfTemplateBytes, Dictionary<string, string> templateData)
    {
        using (var ms = new MemoryStream())
        {
            var pdfReader = new PdfReader(new MemoryStream(pdfTemplateBytes));
            var pdfWriter = new PdfWriter(ms);
            var pdfDoc = new PdfDocument(pdfReader, pdfWriter);

            for (var pageNum = 1; pageNum <= pdfDoc.GetNumberOfPages(); pageNum++)
            {
                var page = pdfDoc.GetPage(pageNum);
                var pdfCanvas = new PdfCanvas(page);

                // Extract text from the page
                var strategy = new LocationTextExtractionStrategy();
                PdfCanvasProcessor parser = new PdfCanvasProcessor(strategy);
                parser.ProcessPageContent(page);

                var text = strategy.GetResultantText();

                // Replace placeholders with actual data
                foreach (var keyValuePair in templateData)
                {
                    text = text.Replace($"{{{keyValuePair.Key}}}", keyValuePair.Value);
                }

                // Clear the existing content on the page
                pdfCanvas.GetContentStream().GetOutputStream().Write(Encoding.UTF8.GetBytes(""));

                // Write the modified text to the page
                var modifiedBytes = Encoding.UTF8.GetBytes(text);
                pdfCanvas.GetContentStream().GetOutputStream().Write(modifiedBytes, 0, modifiedBytes.Length);
            }

            pdfDoc.Close();
            return ms.ToArray();
        }
    }
}