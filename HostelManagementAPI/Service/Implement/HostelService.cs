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

        public HostelService(
            IHostelRepository hostelRepository,
            IAccountRepository accountRepository,
            ICloudinaryService cloudinaryService)
        {
            _hostelRepository = hostelRepository;
            _accountRepository = accountRepository;
            _cloudinaryService = cloudinaryService;
        }

        public async Task ChangeHostelStatus(int hostelId, int status)
        {
            var currentHostel = await _hostelRepository.GetHostelById(hostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (!Enum.IsDefined(typeof(HostelEnum), status))
            {
                throw new ServiceException("Invalid status value");
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

            var newHostelId = await _hostelRepository.CreateHostel(createHostelRequestDto);

            return new CreateHostelResponseDto { HostelID = newHostelId };
        }

        public async Task<HostelResponseDto> GetHostelDetail(int hostelID)
        {
            return await _hostelRepository.GetHostelById(hostelID);
        }

        public async Task<HostelDetailAdminView> GetHostelDetailAdminView(int hostelID)
        {
            return await _hostelRepository.GetHostelDetailAdminView(hostelID);
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
            //if (ownerAccount.Hostels == null)
            //{
            //    return null;
            //}

            return await _hostelRepository.GetOwnerHostels(ownerId);
        }


        public async Task UpdateHostel(int hostelId, UpdateHostelRequestDto updateHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(updateHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            var currentHostel = await _hostelRepository.GetHostelById(hostelId);
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

        public async Task UploadHostelThumbnail(int hostelId, IFormFile formFile)
        {
            var currentHostel = await _hostelRepository.GetHostelById(hostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }
            else
            {
                try
                {
                    var result = await _cloudinaryService.AddPhotoAsync(formFile);
                    if (result.Error != null)
                    {
                        throw new ServiceException("Error uploading image to Cloudinary: " + result.Error.Message);
                    }

                    string imageUrl = result.SecureUrl.AbsoluteUri;
                    currentHostel.Thumbnail = imageUrl;

                    await _hostelRepository.UpdateHostelImage(hostelId, imageUrl);
                }
                catch (Exception ex)
                {
                    throw new ServiceException("Upload hostel image fail with error", ex);
                }
            }
        }
    }
}
