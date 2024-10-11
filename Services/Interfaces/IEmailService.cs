namespace ServiceCollectionAPI.Services.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string toEmail, string templateId, Dictionary<string, string> templateData);
    }
}

