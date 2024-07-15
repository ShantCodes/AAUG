namespace AAUG.Service.Interfaces.EmailSender;

public interface IEmailSenderService
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}
