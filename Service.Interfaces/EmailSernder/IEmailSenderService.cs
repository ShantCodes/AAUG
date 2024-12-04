namespace AAUG.Service.Interfaces.EmailSender;

public interface IEmailSenderService
{
    Task SendEmailAsync(string to, string subject, string body);
}
