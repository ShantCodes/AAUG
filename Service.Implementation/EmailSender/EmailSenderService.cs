using System.Net;
using System.Net.Mail;
using AAUG.DomainModels.Dtos.Email;
using AAUG.Service.Interfaces.EmailSender;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AAUG.Service.Implementations.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    private readonly EmailSettings _emailSettings;

    public EmailSenderService(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        // Set a custom callback to bypass SSL certificate validation
        ServicePointManager.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) => true;

        using (var client = new SmtpClient())
        {
            client.Host = _emailSettings.SmtpHost;
            client.Port = _emailSettings.SmtpPort;
            client.EnableSsl = _emailSettings.EnableSsl;
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_emailSettings.SenderEmail, _emailSettings.SenderName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(to);

            await client.SendMailAsync(mailMessage);
        }
    }

}
