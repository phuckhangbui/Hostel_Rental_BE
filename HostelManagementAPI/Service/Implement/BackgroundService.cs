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
        private readonly IContractRepository _contractRepository;
        private readonly IHostelRepository _hostelRepository;
        private readonly INotificationService _notificationService;
        private readonly IAccountRepository _accountRepository;

        public BackgroundService(ILogger<IBackgroundService> logger, 
            IMembershipRegisterRepository membershipRegisterRepository,
            IContractRepository contractRepository,
            IHostelRepository hostelRepository,
            INotificationService notificationService,
            IAccountRepository accountRepository)
        {
            _logger = logger;
            _membershipRegisterRepository = membershipRegisterRepository;
            _contractRepository = contractRepository;
            _hostelRepository = hostelRepository;
            _notificationService = notificationService;
            _accountRepository = accountRepository;
        }

        public async Task ScheduleContractWhenExpire()
        {
            try
            {
                var currentDateTime = DateTime.Now;

                var signedContracs = await _contractRepository.GetSignedContracs();
                if (signedContracs != null && signedContracs.Any())
                {
                    _logger.LogInformation("Starting background job for ScheduleContractWhenExpire");

                    foreach (var contract in signedContracs)
                    {
                        TimeSpan delayToStart = (TimeSpan)(contract.DateEnd - currentDateTime);

                        BackgroundJob.Schedule(() => UpdateContractToExpired(contract.ContractID), delayToStart);
                        _logger.LogInformation($"Contract id: {contract.ContractID} scheduled for status change: " +
                            $"'Expired' at {delayToStart}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while update contract when expire");
            }
        }

        public async Task UpdateContractToExpired(int contractId)
        {
            var contract = await _contractRepository.GetContractById(contractId);
            var hostel = await _hostelRepository.GetHostelInformation((int)contract.RoomID);
            var accountHiring = await _accountRepository.GetAccountById((int)contract.StudentAccountID);

            if (contract != null && contract.Status == (int)ContractStatusEnum.signed)
            {
                contract.Status = (int)ContractStatusEnum.expired;

                await _contractRepository.UpdateContract(contract);
                _logger.LogInformation($"Contract id: {contractId} updated to 'expired' successfully at {DateTime.Now}.");

                await _notificationService.SendMemberWhenContractExpired(accountHiring.AccountId, accountHiring.FirebaseToken, accountHiring.Name, hostel);
            }
        }

        public async Task ScheduleMembershipWhenExpire()
        {
            try
            {
                var currentDateTime = DateTime.Now;

                var activeMemberships = await _membershipRegisterRepository.GetAllActiveMembership();
                if (activeMemberships != null && activeMemberships.Any())
                {
                    _logger.LogInformation("Starting background job for ScheduleMembershipWhenExpire");

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
