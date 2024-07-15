using System.Net;
using System.Net.Mail;
using AAUG.Service.Interfaces.EmailSender;
using Microsoft.Extensions.Configuration;

namespace AAUG.Service.Implementations.EmailSender;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var client = new SmtpClient
        {
            Host = configuration["EmailSettings:Host"],
            Port = int.Parse(configuration["EmailSettings:Port"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(configuration["EmailSettings:Username"], configuration["EmailSettings:Password"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(configuration["EmailSettings:From"]),
            Subject = subject,
            Body = htmlMessage, 
            IsBodyHtml = true
        };

        mailMessage.To.Add(email);

        await client.SendMailAsync(mailMessage);
    }
}
