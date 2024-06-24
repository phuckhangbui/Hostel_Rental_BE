namespace Service.Interface
{
    public interface IBackgroundService
    {
        Task ScheduleMembershipWhenExpire();
        Task ScheduleContractWhenExpire();
    }
}
