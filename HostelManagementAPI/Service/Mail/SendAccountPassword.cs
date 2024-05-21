namespace Service.Mail
{
    public class SendAccountPassword
    {
        public static MailContent SendInitPassword(string toEmail, string initPassword)
        {
            var mailContext = new MailContent();
            mailContext.To = toEmail;
            mailContext.Subject = $"Welcome to ......";
            mailContext.Body = $@"<p>Dear Participant,</p>
            <p>Here is your opt: {initPassword}</p>
            <p>REAS - Real Estate Auction Platform</p>";
            return mailContext;
        }

        public static MailContent SendTempPasswordForForgotUser(string toEmail, string newPassword)
        {
            var mailContext = new MailContent();
            mailContext.To = toEmail;
            mailContext.Subject = $"Welcome to HealthFeast";
            mailContext.Body = $@"<p>Dear Participant,</p>
            <p>Here is your temporary password: {newPassword}</p>
            <p>REAS - Real Estate Auction Platform</p>";
            return mailContext;
        }
    }
}
