using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CEN4020_Website.Pages.Services
{
    public class SendGridEmailSender : IEmailSender
    {
        public SendGridEmailSender(
            IOptions<ResetMessageSenderOptions> options
            )
        {
            this.Options = options.Value;
        }

        public ResetMessageSenderOptions Options { get; set; }

        public async Task SendEmailAsync(
            string email,
            string subject,
            string message)
        {
            await Execute(Options.SendGridKey, subject, message, email);
        }
        //Uses our SendGrid API Key to build the email and send it using the specified email
        private async Task<Response> Execute(
            string SendGridAPIKey,
            string subject,
            string message,
            string email)
        {
            var client = new SendGridClient(SendGridAPIKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress(Options.SenderEmail, Options.SenderName),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message
            };
            //Adds the email to recieve the message
            msg.AddTo(new EmailAddress(email));

            // disable tracking settings
            msg.SetClickTracking(false, false);
            msg.SetOpenTracking(false);
            msg.SetGoogleAnalytics(false);
            msg.SetSubscriptionTracking(false);

            return await client.SendEmailAsync(msg);
        }
    }
}