using DTOs.Enum;

namespace DTOs
{
    public class NotificationDto
    {
        public int? NotificationId { get; set; }
        public int? AccountNoticeId { get; set; }
        public int? ReceiveAccountId { get; set; }
        public string? NotificationText { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? NotificationType { get; set; }
        public string? Title { get; set; }
        public string? ForwardToPath { get; set; }
        public bool? IsRead { get; set; }


        public static string GetForwardPath(int notificationType)
        {
            switch (notificationType)
            {
                case (int)NotificationTypeEnum.owner_create_contract:
                    return "/owner/contracts";

                case (int)NotificationTypeEnum.owner_create_bill:
                    return "/owner/bills";

                case (int)NotificationTypeEnum.member_sign_contract:
                    return "/member/contracts";

                case (int)NotificationTypeEnum.member_pay_bill:
                    return "/member/bills";

                case (int)NotificationTypeEnum.member_make_appointment:
                    return "/member/appointments";

                case (int)NotificationTypeEnum.member_rent_room:
                    return "/member/rooms";

                case (int)NotificationTypeEnum.member_send_complain:
                    return "/member/complains";

                case (int)NotificationTypeEnum.owner_reply_complain:
                    return "/owner/complains";

                case (int)NotificationTypeEnum.owner_package_expire:
                    return "/owner/packages";

                case (int)NotificationTypeEnum.contract_finish:
                    return "/contracts";

                case (int)NotificationTypeEnum.owner_create_contract_but_this_is_for_decline_member:
                    return "/member/contracts";

                case (int)NotificationTypeEnum.member_decline_contract:
                    return null;

                default:
                    return null;
            }
        }
    }
}
