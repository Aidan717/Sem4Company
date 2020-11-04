using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Web_API_Service.Models;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;

namespace Web_API_Service.Service {
    public class MailService : IMailService {

		//MailSettings mSettings = new MailSettings() {
		//MailReciever = "gruppe6testmail@gmail.com",
		//    MailSender = "gruppe6testmail@gmail.com",
		//    DisplayName = "Test Email",
		//    Password = "password424",
		//    Host = "smtp.gmail.com",
		//    Port = 587
		//};
		//public MailService() {
		//
		//}
		private readonly MailSettings _mailSettings;



		public MailService(IOptions<MailSettings> mailSettings) {
			_mailSettings = mailSettings.Value;
		}

		//public async Task SendEmailAsync(MailRequest mailRequest) {
		//    var email = new MimeMessage();
		//    email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
		//    email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
		//    email.Subject = mailRequest.Subject;
		//    var builder = new BodyBuilder();
		//    if (mailRequest.Attachments != null) {
		//        byte[] fileBytes;
		//        foreach (var file in mailRequest.Attachments) {
		//            if (file.Length > 0) {
		//                using (var ms = new MemoryStream()) {
		//                    file.CopyTo(ms);
		//                    fileBytes = ms.ToArray();
		//                }
		//                builder.Attachments.Add(file.FileName, fileBytes, ContentType.Parse(file.ContentType));
		//            }
		//        }
		//    }
		//    builder.HtmlBody = mailRequest.Body;
		//    email.Body = builder.ToMessageBody();
		//    using var smtp = new SmtpClient();
		//    smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
		//    smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
		//    await smtp.SendAsync(email);
		//    smtp.Disconnect(true);
		//}

		//public async Task SendWelcomeEmailAsync(WelcomeRequest request) {
		//	string FilePath = Directory.GetCurrentDirectory() + "\\Templates\\WelcomeTemplate.html";
		//	StreamReader str = new StreamReader(FilePath);
		//	string MailText = str.ReadToEnd();
		//	str.Close();
		//	MailText = MailText.Replace("[username]", request.UserName).Replace("[email]", request.ToEmail);
		//	var email = new MimeMessage();
		//	email.Sender = MailboxAddress.Parse(_mailSettings.Mail);
		//	email.To.Add(MailboxAddress.Parse(request.ToEmail));
		//	email.Subject = $"Welcome {request.UserName}";
		//	var builder = new BodyBuilder();
		//	builder.HtmlBody = MailText;
		//	email.Body = builder.ToMessageBody();
		//	using var smtp = new SmtpClient();
		//	smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
		//	smtp.Authenticate(_mailSettings.Mail, _mailSettings.Password);
		//	await smtp.SendAsync(email);
		//	smtp.Disconnect(true);
		//}

		public async Task SendWarningEmailAsync(string methodName, string Query, string Destination, string Error) {
           // IOptions<MailSettings> _mailSettings = Options.Create(mSettings);
           // MailSettings mailSettings = _mailSettings.Value;

            MailRequest warningRequest = new MailRequest();
            var email = new MimeMessage();
            var builder = new BodyBuilder();

            warningRequest.Body += "Method:<br />" + methodName;
            warningRequest.Body += "<br /><br />Query:<br />" + Query;
            warningRequest.Body += "<br /><br />Destination:<br />" + Destination;
            warningRequest.Body += "<br /><br />Error message:<br />" + Error;

            builder.HtmlBody = warningRequest.Body;

            email.Sender = MailboxAddress.Parse(_mailSettings.MailReciever);
            //email.To.Add(MailboxAddress.Parse(warningRequest.ToEmail));
            email.To.Add(MailboxAddress.Parse(_mailSettings.MailSender));
            //email.Subject = warningRequest.Subject;
            email.Subject = "Error for HTTPRequest";
            email.Body = builder.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.Connect(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSettings.MailReciever, _mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }
    }
 }