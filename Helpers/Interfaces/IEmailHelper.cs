public interface IEmailHelper
{
    Task SendEmailAsync(string fromEmail, string toEmail, string subject, string templateId, Dictionary<string, string> templateData);
}