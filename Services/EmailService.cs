using ServiceCollectionAPI.Services.Interfaces;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ServiceCollectionAPI.Services
{
    public class EmailService : IEmailService
    {
        private readonly string apiKey;
        private readonly string senderEmail;

        public EmailService(string apiKey, string senderEmail)
        {
            this.apiKey = apiKey;
            this.senderEmail = senderEmail;
        }

        public async Task SendEmailAsync(string toEmail, string templateId, Dictionary<string, string> templateData)
        {
            var client = new SendGridClient(apiKey);
            var msg = new SendGridMessage
            {
                From = new EmailAddress(senderEmail, "Your Name"),
                Subject = "Subject of the Email",
                HtmlContent = "<strong>Placeholder content. This will be replaced by the template.</strong>"
            };

            msg.AddTo(new EmailAddress(toEmail));

            // Add template data
            foreach (var kvp in templateData)
            {
                msg.AddSubstitution(kvp.Key, kvp.Value);
            }

            msg.SetTemplateId(templateId);

            var response = await client.SendEmailAsync(msg);

            if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
            {
                throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
            }
        }
    }
}

