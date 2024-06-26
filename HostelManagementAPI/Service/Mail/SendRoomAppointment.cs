namespace Service.Mail
{
    public class SendRoomAppointment
    {
        public static MailContent SendViewingAppointmentNotification(string toEmail, string ownerName, string roomName, string appointmentDate)
        {
            var mailContent = new MailContent();
            mailContent.To = toEmail;
            mailContent.Subject = "Upcoming Room Viewing Appointment Notification";
            mailContent.Body = $@"<p>Dear {ownerName},</p>
            <p>I hope this email finds you well.</p>
            <p>I am writing to inform you that there is an upcoming viewing appointment scheduled for your hostel. Please find the details below:</p>
            <p><strong>Appointment Details:</strong></p>
            <p><strong>Room Name:</strong> {roomName}<br>
            <strong>Date:</strong> {appointmentDate}<br>
            <p>We kindly ask you to ensure that the room is ready and available for the viewing at the scheduled time. If there are any issues or changes that need to be addressed, please do not hesitate to contact us as soon as possible.</p>
            <p>Thank you for your attention to this matter.</p>
            <p>Best regards,</p>
            <p>[Airestates]<br>";
            return mailContent;
        }

        public static MailContent SendViewingHiringDirectlyNotification(string toEmail, string ownerName, string roomName, string appointmentDate)
        {
            var mailContent = new MailContent();
            mailContent.To = toEmail;
            mailContent.Subject = "Upcoming Room Viewing Appointment Notification";
            mailContent.Body = $@"<p>Dear {ownerName},</p>
            <p>I hope this email finds you well.</p>
            <p>I am writing to inform you that there is an upcoming hiring directly for your hostel. Please find the details below:</p>
            <p><strong>Appointment Details:</strong></p>
            <p><strong>Room Name:</strong> {roomName}<br>
            <strong>Date:</strong> {appointmentDate}<br>
            <p>We kindly ask you to ensure that the room is ready and available for the contract. If there are any issues or changes that need to be addressed, please do not hesitate to contact us as soon as possible.</p>
            <p>Thank you for your attention to this matter.</p>
            <p>Best regards,</p>
            <p>[Airestates]<br>";
            return mailContent;
        }
    }
}
