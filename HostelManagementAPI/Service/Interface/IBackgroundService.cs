namespace Service.Interface
{
    public interface IBackgroundService
    {
        Task ScheduleMembershipWhenExpire();
    }
}
