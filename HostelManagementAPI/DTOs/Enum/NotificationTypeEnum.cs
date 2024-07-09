namespace DTOs.Enum
{
    public enum NotificationTypeEnum
    {
        owner_create_contract = 1,
        owner_create_bill = 2,
        member_sign_contract = 3,
        member_pay_bill = 4,
        member_make_appointment = 5,
        member_rent_room = 6,
        member_send_complain = 7,
        owner_reply_complain = 8,
        owner_package_expire = 9,
        contract_finish = 10, //send for both member and owner
        owner_create_contract_but_this_is_for_decline_member = 11,
        member_decline_contract = 12,
        member_make_hiring_request = 13,
        member_contract_expired = 14,
    }

}
