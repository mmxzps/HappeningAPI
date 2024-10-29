using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailSender : IEmailSender
{
    public string SmtpServer { get; }
    public int SmtpPort { get; }
    public string SmtpUser { get; }
    public string SmtpPass { get; }

    public EmailSender(string smtpServer, int smtpPort, string smtpUser, string smtpPass)
    {
        SmtpServer = smtpServer;
        SmtpPort = smtpPort;
        SmtpUser = smtpUser;
        SmtpPass = smtpPass;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(SmtpUser),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        using (var client = new SmtpClient(SmtpServer, SmtpPort))
        {
            client.Credentials = new NetworkCredential(SmtpUser, SmtpPass);
            client.EnableSsl = true;

            try
            {
                await client.SendMailAsync(mailMessage);
            }
            catch (SmtpException smtpEx)
            {
                throw new Exception($"Email could not be sent. SMTP Exception: {smtpEx.Message}", smtpEx);
            }
            catch (Exception ex)
            {
                throw new Exception($"Email could not be sent. Exception: {ex.Message}", ex);
            }
        }
    }
}

