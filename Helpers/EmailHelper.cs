using SendGrid;
using SendGrid.Helpers.Mail;

public class EmailHelper: IEmailHelper
{
    private readonly IConfiguration _configuration;

    public EmailHelper(IConfiguration connfiguration)
    {
        _configuration = connfiguration;
    }

    public async Task SendEmailAsync(string fromEmail, string toEmail, string subject, string templateId, Dictionary<string, string> templateData)
    {
        var client = new SendGridClient(_configuration["SENDGRID_API"]);
        var from = new EmailAddress(fromEmail);
        var to = new EmailAddress(toEmail);
        var msg = new SendGridMessage();
        msg.SetFrom(from);
        msg.AddTo(to);
        msg.SetSubject(subject);
        msg.SetTemplateId(templateId);
        msg.SetTemplateData(templateData);
        var response = await client.SendEmailAsync(msg);

        if (response.StatusCode != System.Net.HttpStatusCode.Accepted)
        {
            throw new Exception($"Failed to send email. Status code: {response.StatusCode}");
        }
    }
}