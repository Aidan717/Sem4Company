using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using Microsoft.Extensions.DependencyInjection;
using MimeKit;
using Web_API_Service.Models;

namespace Web_API_Service.util {
    public class Email {

        public void ConfigureServices(IServiceCollection services) {
            var notificationMetadata =
            Configuration.GetSection("NotificationMetadata").
            Get<NotificationMetaData>();
            services.AddSingleton(notificationMetadata);
            services.AddControllers();
        }

        //The following method illustrates how you can create a MimeMessage instance from an instance of our custom class EmailMessage.
        private MimeMessage CreateMimeMessageFromEmailMessage(EmailMessage message) {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(message.Sender);
            mimeMessage.To.Add(message.Reciever);
            mimeMessage.Subject = message.Subject;
            mimeMessage.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = message.Content };
            return mimeMessage;
        }

        public string Get() {
            EmailMessage message = new EmailMessage();
            message.Sender = new MailboxAddress("Self", _notificationMetadata.Sender);
            message.Reciever = new MailboxAddress("Self", _notificationMetadata.Reciever);
            message.Subject = "Welcome";
            message.Content = "Hello World!";
            var mimeMessage = CreateEmailMessage(message);
            using (SmtpClient smtpClient = new SmtpClient()) {
                smtpClient.Connect(_notificationMetadata.SmtpServer,
                _notificationMetadata.Port, true);
                smtpClient.Authenticate(_notificationMetadata.UserName,
                _notificationMetadata.Password);
                smtpClient.Send(mimeMessage);
                smtpClient.Disconnect(true);
            }
            return "Email sent successfully";
        }
    }
}