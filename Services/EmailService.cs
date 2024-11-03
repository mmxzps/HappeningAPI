using Azure.Communication.Email;
using EventVault.Services.IServices;
using Microsoft.AspNetCore.Http.HttpResults;

namespace EventVault.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailClient _emailClient;

        public EmailService(EmailClient emailClient)
        {
            _emailClient = emailClient;
        }

        public async Task<bool> SendEmailAsync(string toEmail, string subject, string htmlContent)
        {
            try
            {
                var emailMessage = new EmailMessage(
                    senderAddress: "DoNotReply@bf49f060-a08a-4a63-bab5-9703cd5613c4.azurecomm.net",
                    content: new EmailContent(subject)
                    {
                        Html = htmlContent
                    },
            recipients: new EmailRecipients(new List<EmailAddress> { new EmailAddress(toEmail) }
            ));

                var response = await _emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);

                return response.HasCompleted;
            }

            catch (Exception)
            {
                return false;
            }
        }
    }
}
