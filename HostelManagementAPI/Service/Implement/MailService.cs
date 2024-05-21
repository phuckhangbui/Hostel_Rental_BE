using BusinessObject.Mail;
using MailKit.Security;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using Service.Interface;

namespace Service.Implement
{
    public class MailService : IMailService
    {
        private MailSetting setting { get; set; }
        public MailService()
        {
            setting.Mail = "reasspring2024@gmail.com";
            setting.Host = "smtp.gmail.com";
            setting.Port = 587;
            setting.Passwork = "zgtj veex szof becd";
            setting.DisplayName = "Hostel Management Platform";
        }


        public void SendMailConfig(MailContent mailContent)
        {
            var email = new MimeMessage();
            email.Sender = new MailboxAddress(setting.DisplayName, setting.Mail);
            email.From.Add(new MailboxAddress(setting.DisplayName, setting.Mail));
            email.To.Add(new MailboxAddress(mailContent.To, mailContent.To));
            email.Subject = mailContent.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailContent.Body;
            email.Body = builder.ToMessageBody();

            using var smtpClient = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                smtpClient.Connect(setting.Host, setting.Port, SecureSocketOptions.StartTls);
                smtpClient.Authenticate(setting.Mail, setting.Passwork);
                smtpClient.Send(email);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            smtpClient.Disconnect(true);
        }

        public void SendMailToAuthencationOTP(string email)
        {
            MailContent mailContent = new MailContent();
            mailContent.To = email.ToString();
            mailContent.Subject = $"AUTHENTICATION OTP";
            mailContent.Body = "";
            SendMailConfig(mailContent);
        }
    }
}
