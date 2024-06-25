using DTOs;
using DTOs.Hostel;

namespace Service.Interface
{
    public interface INotificationService
    {
        Task<List<NotificationDto>> GetAllNotifications();
        Task CreateNotification(NotificationDto notificationDto);
        Task<List<NotificationDto>> GetNotificationsBaseOnReceiveId(int accountId);
        Task SendMemberWhoGetNewContract(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendMembersWhoGetDeclineContract(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendOwnerWhenMemberDepositContract(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendOwnerWhenMemberPayMonthlyFee(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendMemberWhenOwnerCreateNewMonthlyPayment(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendOwnerWhenMemberComplain(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse, string complain);
        Task SendMemberWhenOwnerReplyComplain(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse, string complainResponse);
        Task SendOwnerWhenMemberMakeAppointment(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);
        Task SendOwnerWhenMemberRentRoom(int accountReceivedId, string? firebaseToken, string name, InformationHouse informationHouse);

    }
}
