using DTOs.Hostel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Mail
{
    public class SendMailUserHiring
    {
        public static MailContent SendMailWithUserHiringSuccess(string toEmail, string name, InformationHouse informationHouse)
        {
            var mailContext = new MailContent();
            mailContext.To = toEmail;
            mailContext.Subject = "CONGRATULATIONS ON YOUR SUCCESSFULLY RENTING A ROOM";
            var bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("<!DOCTYPE html>");
            bodyBuilder.AppendLine("<html lang='en'>");
            bodyBuilder.AppendLine("<head>");
            bodyBuilder.AppendLine("<meta charset='UTF-8' />");
            bodyBuilder.AppendLine("<title>HOSTEL FLATFORM</title>");
            bodyBuilder.AppendLine("<style>");
            bodyBuilder.AppendLine("body {margin: 0; padding: 0; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #333; background-color: #fff;}");
            bodyBuilder.AppendLine(".container {margin: 0 auto; width: 100%; max-width: 600px; padding: 0 0px; padding-bottom: 10px; border-radius: 5px; line-height: 1.8;}");
            bodyBuilder.AppendLine(".header {border-bottom: 1px solid #eee; display: flex;}");
            bodyBuilder.AppendLine(".img {margin-top: 20px; margin-left: 20px;}");
            bodyBuilder.AppendLine(".header a {font-size: 1.4em; color: #000; text-decoration: none; font-weight: 600;}");
            bodyBuilder.AppendLine(".content {min-width: 700px; overflow: auto; line-height: 2;}");
            bodyBuilder.AppendLine(".otp {background: linear-gradient(to right, #00bc69 0, #00bc88 50%, #00bca8 100%); margin: 0 auto; width: max-content; padding: 0 10px; color: #fff; border-radius: 4px;}");
            bodyBuilder.AppendLine(".footer {color: #aaa; font-size: 0.8em; line-height: 1; font-weight: 300;}");
            bodyBuilder.AppendLine(".email-info {color: #666666; font-weight: 400; font-size: 13px; line-height: 18px; padding-bottom: 6px;}");
            bodyBuilder.AppendLine(".email-info a {text-decoration: none; color: #00bc69;}");
            bodyBuilder.AppendLine("</style>");
            bodyBuilder.AppendLine("</head>");
            bodyBuilder.AppendLine("<body>");
            bodyBuilder.AppendLine("<div class='container'>");
            bodyBuilder.AppendLine("<div class='header'>");
            bodyBuilder.AppendLine("<div class='img'>");
            bodyBuilder.AppendLine("<img src='https://res.cloudinary.com/newdawn/image/upload/c_scale,h_120,w_120/hylocmmy9gcsbo3brvdh.jpg' alt='LogoImage'/>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<div>");
            bodyBuilder.AppendLine("<h1>HOSTEL FLATFORM</h1>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<br />");

            bodyBuilder.AppendLine($"<p>Dear {name},</p>");
            bodyBuilder.AppendLine($"<p>First of all, congratulations on renting at room {informationHouse.RoomName} house {informationHouse.HostelName} at address {informationHouse.Address}.</p>");

            bodyBuilder.AppendLine("<p>Your contract has been successfully created. Please go to the homepage with your login account to register for the contract.</p>");

            bodyBuilder.AppendLine("<p>Wish you have a nice day!</p>");

            bodyBuilder.AppendLine("<hr style='border: none; border-top: 0.5px solid #131111' />");
            bodyBuilder.AppendLine("<div class='footer'>");
            bodyBuilder.AppendLine("<p>This email can't receive replies.</p>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</body>");
            bodyBuilder.AppendLine("</html>");

            mailContext.Body = bodyBuilder.ToString();
            return mailContext;
        }

        public static MailContent SendEmailDeclineAppointment(string toEmail, string name, InformationHouse informationHouse)
        {
            var mailContext = new MailContent();
            mailContext.To = toEmail;
            mailContext.Subject = "REFUSE TO VIEW THE ROOM";
            var bodyBuilder = new StringBuilder();
            bodyBuilder.AppendLine("<!DOCTYPE html>");
            bodyBuilder.AppendLine("<html lang='en'>");
            bodyBuilder.AppendLine("<head>");
            bodyBuilder.AppendLine("<meta charset='UTF-8' />");
            bodyBuilder.AppendLine("<title>HOSTEL FLATFORM</title>");
            bodyBuilder.AppendLine("<style>");
            bodyBuilder.AppendLine("body {margin: 0; padding: 0; font-family: 'Helvetica Neue', Helvetica, Arial, sans-serif; color: #333; background-color: #fff;}");
            bodyBuilder.AppendLine(".container {margin: 0 auto; width: 100%; max-width: 600px; padding: 0 0px; padding-bottom: 10px; border-radius: 5px; line-height: 1.8;}");
            bodyBuilder.AppendLine(".header {border-bottom: 1px solid #eee; display: flex;}");
            bodyBuilder.AppendLine(".img {margin-top: 20px; margin-left: 20px;}");
            bodyBuilder.AppendLine(".header a {font-size: 1.4em; color: #000; text-decoration: none; font-weight: 600;}");
            bodyBuilder.AppendLine(".content {min-width: 700px; overflow: auto; line-height: 2;}");
            bodyBuilder.AppendLine(".otp {background: linear-gradient(to right, #00bc69 0, #00bc88 50%, #00bca8 100%); margin: 0 auto; width: max-content; padding: 0 10px; color: #fff; border-radius: 4px;}");
            bodyBuilder.AppendLine(".footer {color: #aaa; font-size: 0.8em; line-height: 1; font-weight: 300;}");
            bodyBuilder.AppendLine(".email-info {color: #666666; font-weight: 400; font-size: 13px; line-height: 18px; padding-bottom: 6px;}");
            bodyBuilder.AppendLine(".email-info a {text-decoration: none; color: #00bc69;}");
            bodyBuilder.AppendLine("</style>");
            bodyBuilder.AppendLine("</head>");
            bodyBuilder.AppendLine("<body>");
            bodyBuilder.AppendLine("<div class='container'>");
            bodyBuilder.AppendLine("<div class='header'>");
            bodyBuilder.AppendLine("<div class='img'>");
            bodyBuilder.AppendLine("<img src='https://res.cloudinary.com/newdawn/image/upload/c_scale,h_120,w_120/hylocmmy9gcsbo3brvdh.jpg' alt='LogoImage' />");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<div>");
            bodyBuilder.AppendLine("<h1>HOSTEL FLATFORM</h1>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("<br />");

            bodyBuilder.AppendLine($"<p>Dear {name},</p>");
            bodyBuilder.AppendLine($"<p>First of all, I would like to apologize for booking a room viewing appointment at room {informationHouse.RoomName} house {informationHouse.HostelName} at address {informationHouse.Address}.</p>");

            bodyBuilder.AppendLine("<p>Your home viewing schedule has been rejected because someone before you agreed to rent the house. I want to apologize to you for this inconvenience.</p>");

            bodyBuilder.AppendLine("<p>Wish you have a nice day!</p>");

            bodyBuilder.AppendLine("<hr style='border: none; border-top: 0.5px solid #131111' />");
            bodyBuilder.AppendLine("<div class='footer'>");
            bodyBuilder.AppendLine("<p>This email can't receive replies.</p>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</div>");
            bodyBuilder.AppendLine("</body>");
            bodyBuilder.AppendLine("</html>");

            mailContext.Body = bodyBuilder.ToString();
            return mailContext;
        }
    }
}
