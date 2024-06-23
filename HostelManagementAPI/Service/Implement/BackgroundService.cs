using Microsoft.Extensions.Logging;
using Repository.Interface;
using Service.Interface;
using Hangfire;
using DTOs.Enum;

namespace Service.Implement
{
    public class BackgroundService : IBackgroundService
    {
        private readonly ILogger<IBackgroundService> _logger;
        private readonly IMembershipRegisterRepository _membershipRegisterRepository;

        public BackgroundService(ILogger<IBackgroundService> logger, IMembershipRegisterRepository membershipRegisterRepository)
        {
            _logger = logger;
            _membershipRegisterRepository = membershipRegisterRepository;

        }

        public async Task ScheduleMembershipWhenExpire()
        {
            try
            {
                var currentDateTime = DateTime.Now;

                var activeMemberships = await _membershipRegisterRepository.GetAllActiveMembership();
                if (activeMemberships != null && activeMemberships.Any())
                {
                    _logger.LogInformation("Starting background job for UpdateMembershipWhenExpire");

                    foreach (var membership in activeMemberships)
                    {
                        if (membership != null)
                        {
                            TimeSpan delayToStart = (TimeSpan)(membership.DateExpire - currentDateTime);

                            BackgroundJob.Schedule(() => UpdateMembershipRegisterToExpired(membership.MemberShipTransactionID), delayToStart);
                            _logger.LogInformation($"Membership id: {membership.MemberShipTransactionID} scheduled for status change: " +
                                $"'Expired' at {delayToStart}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while update membership when expire");
            }
        }

        public async Task UpdateMembershipRegisterToExpired(int? membershipRegisterId)
        {
            if (membershipRegisterId != null)
            {
                var membershipRegister = await _membershipRegisterRepository.GetMemberShipRegisterTransactionById((int)membershipRegisterId);
                if (membershipRegister != null)
                {
                    membershipRegister.Status = (int)MembershipRegisterEnum.expired;

                    await _membershipRegisterRepository.UpdateMembership(membershipRegister);
                    _logger.LogInformation($"Membership id: {membershipRegisterId} updated to 'expired' successfully at {DateTime.Now}.");
                }
            }
        }
    }
}
