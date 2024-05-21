using BusinessObject.Mail;

namespace Service.Interface
{
    public interface IMailService
    {
        void SendMailConfig(MailContent mailContent);
        void SendMailToAuthencationOTP(string email);
    }
}
