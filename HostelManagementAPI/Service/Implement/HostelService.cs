using DTOs.Enum;
using DTOs.Hostel;
using Microsoft.AspNetCore.Http;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class HostelService : IHostelService
    {
        private readonly IHostelRepository _hostelRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly IMembershipRegisterRepository _membershipRegisterRepository;

        public HostelService(
            IHostelRepository hostelRepository,
            IAccountRepository accountRepository,
            ICloudinaryService cloudinaryService,
            IMembershipRegisterRepository membershipRegisterRepository)
        {
            _hostelRepository = hostelRepository;
            _accountRepository = accountRepository;
            _cloudinaryService = cloudinaryService;
            _membershipRegisterRepository = membershipRegisterRepository;
        }

        public async Task ChangeHostelStatus(int hostelId, int status)
        {
            var currentHostel = await _hostelRepository.GetHostelDetailById(hostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (!Enum.IsDefined(typeof(HostelEnum), status))
            {
                throw new ServiceException("Invalid status value");
            }

            if (status == (int)HostelEnum.Available && currentHostel.NumOfAvailableRoom == 0)
            {
                throw new ServiceException("Can not change hostel status to available because no room is available");
            }

            await _hostelRepository.UpdateHostelStatus(hostelId, status);
        }

        public async Task<CreateHostelResponseDto> CreateHostel(CreateHostelRequestDto createHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(createHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            if (!Enum.TryParse(typeof(HostelTypeEnum), createHostelRequestDto.HostelType, out var hostelTypeEnum) || !Enum.IsDefined(typeof(HostelTypeEnum), hostelTypeEnum))
            {
                throw new ServiceException("Invalid hostel type provided.");
            }

            var currentHostels = await _hostelRepository.GetOwnerHostels(ownerAccount.AccountId);
            var currentMemberShip = await _membershipRegisterRepository.GetCurrentActiveMembership(ownerAccount.AccountId);
            if (currentMemberShip != null && currentMemberShip.CapacityHostel == currentHostels.Count())
            {
                throw new ServiceException("You reach maximum number of hostel can create. Please update your pagekage.");
            }

            var newHostelId = await _hostelRepository.CreateHostel(createHostelRequestDto);

            return new CreateHostelResponseDto { HostelID = newHostelId };
        }

        public async Task<HostelResponseDto> GetHostelDetail(int hostelID)
        {
            var hostelResponseDto = await _hostelRepository.GetHostelDetailById(hostelID);
            if (hostelResponseDto != null)
            {
                return hostelResponseDto;
            }
            else
            {
                throw new ServiceException("Hostel not found with this ID");
            }
        }

        public async Task<HostelDetailAdminView> GetHostelDetailAdminView(int hostelID)
        {
            return await _hostelRepository.GetHostelDetailAdminView(hostelID);
        }

        public async Task<HostelResponseDto> GetHostelDetailForOwner(int hostelID)
        {
            var hostelResponseDto = await _hostelRepository.GetHostelDetailById(hostelID);
            if (hostelResponseDto != null)
            {
                return hostelResponseDto;
            }
            else
            {
                throw new ServiceException("Hostel not found with this ID");
            }
        }

        public async Task<IEnumerable<HostelResponseDto>> GetHostels()
        {
            return await _hostelRepository.GetAllHostels();
        }

        public async Task<IEnumerable<HostelsAdminView>> GetHostelsAdminView()
        {
            return await _hostelRepository.GetHostelsAdminView();
        }

        public async Task<IEnumerable<HostelResponseDto>> GetHostelsByOwner(int ownerId)
        {
            var ownerAccount = await _accountRepository.GetAccountWithHostelById(ownerId);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            return await _hostelRepository.GetOwnerHostels(ownerId);
        }


        public async Task UpdateHostel(int hostelId, UpdateHostelRequestDto updateHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(updateHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            var currentHostel = await _hostelRepository.GetHostelDetailById(hostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (currentHostel.AccountID != updateHostelRequestDto.AccountID)
            {
                throw new ServiceException("The hostel does not belong to the specified account.");
            }

            await _hostelRepository.UpdateHostel(hostelId, updateHostelRequestDto);
        }

        public async Task UploadHostelImages(int hostelId, IFormFileCollection files)
        {
            var currentHostel = await _hostelRepository.GetHostelDetailById(hostelId);
            if (currentHostel != null)
            {
                try
                {
                    var images = new List<string>();
                    foreach (var file in files)
                    {
                        var result = await _cloudinaryService.AddPhotoAsync(file);
                        if (result.Error != null)
                        {
                            throw new ServiceException("Error uploading image to Cloudinary: " + result.Error.Message);
                        }

                        string imageUrl = result.SecureUrl.AbsoluteUri;
                        images.Add(imageUrl);
                    }

                    await _hostelRepository.UpdateHostelImage(hostelId, images);
                }
                catch (Exception ex)
                {
                    throw new ServiceException("Upload room image fail with error", ex);
                }
            }
            else
            {
                throw new ServiceException("Hostel not found with this ID");
            }
        }
    }
}
