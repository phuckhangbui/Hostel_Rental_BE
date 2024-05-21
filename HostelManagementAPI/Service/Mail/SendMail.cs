
namespace Service.Mail
{
    public class SendMailAuthentication
    {
        public static MailContent SendMailToAuthencationOTP(string email, string tempOtp)
        {
            MailContent mailContent = new MailContent();
            mailContent.To = email.ToString();
            mailContent.Subject = $"AUTHENTICATION OTP";
            mailContent.Body = $"{tempOtp}";
            return mailContent;
        }
    }
}
