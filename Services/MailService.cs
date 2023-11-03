

using System.Net.Mail;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Album.Mail {
    public class MailSettings  {
        public string Mail {set;get;} = "";
        public string DisplayName {set;get;} = "";
        public string Password {set;get;} = "";
        public string Host {set;get;} = "";
        public int Port {set;get;}
    }

    public class SendMailService : IEmailSender {

        private readonly MailSettings mailSettings ;
        private readonly ILogger logger ;
        public SendMailService ( IOptions<MailSettings> _mailSettings , ILogger<SendMailService> _logger) {
            mailSettings = _mailSettings.Value;
            logger = _logger;
            logger.LogInformation("Create SendMail Service");
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            MimeMessage message = new MimeMessage();
            message.Sender = new MailboxAddress(mailSettings.DisplayName, mailSettings.Mail);
            message.From.Add(message.Sender);
            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            BodyBuilder builder1 = new BodyBuilder();
            builder1.HtmlBody = htmlMessage;
            message.Body = builder1.ToMessageBody();

            using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
            try {
                smtpClient.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(mailSettings.Mail, mailSettings.Password);
                await smtpClient.SendAsync(message) ;
            } catch (Exception ex) {
                // Gửi mail thất bại, nội dung email sẽ lưu vào thư mục mailssave
                System.IO.Directory.CreateDirectory ("mailssave");
                var emailsavefile = string.Format (@"mailssave/{0}.eml", Guid.NewGuid ());
                await message.WriteToAsync (emailsavefile);

                logger.LogInformation ("Lỗi gửi mail, lưu tại - " + emailsavefile);
                logger.LogError (ex.Message);
            }
            smtpClient.Disconnect(true);

            logger.LogInformation ("send mail to: " + email);

        }
    }
}