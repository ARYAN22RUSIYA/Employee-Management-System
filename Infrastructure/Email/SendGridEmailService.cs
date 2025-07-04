using SendGrid;
using SendGrid.Helpers.Mail;
using Microsoft.Extensions.Configuration;
using Core.Interface;
using System.Net.Mail;

public class SendGridEmailService : IEmailService
{
    private readonly string _apiKey;
    private readonly string _fromEmail;

    public SendGridEmailService(IConfiguration configuration)
    {
        _apiKey = configuration["SendGrid:ApiKey"];
        _fromEmail = configuration["SendGrid:FromEmail"];
    }

    public async Task SendEmailAsync(string toEmail, string subject, string plainTextContent = null, string htmlContent = null, string templateId = null, object templateData = null)
    {
        var client = new SendGridClient(_apiKey);
        var from = new EmailAddress(_fromEmail, "EMS_ARYAN");
        var to = new EmailAddress(toEmail);

        SendGridMessage msg;
        if (!string.IsNullOrEmpty(templateId))
        {
            msg = new SendGridMessage();
            msg.SetFrom(from);
            msg.AddTo(to);
            msg.SetTemplateId(templateId);
            msg.SetTemplateData(templateData);
        }
        else
        {
            msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        }

        await client.SendEmailAsync(msg);
    }
}
