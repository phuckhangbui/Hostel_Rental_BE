using AutoMapper;
using BusinessObject.Enum;
using BusinessObject.Models;
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
		private readonly IMapper _mapper;

        public HostelService(
            IHostelRepository hostelRepository,
            IAccountRepository accountRepository,
            ICloudinaryService cloudinaryService,
            IMapper mapper)
        {
            _hostelRepository = hostelRepository;
            _accountRepository = accountRepository;
            _cloudinaryService = cloudinaryService;
            _mapper = mapper;
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

            currentHostel.Status = status;
            await _hostelRepository.UpdateHostel(currentHostel);
        }

        public async Task<CreateHostelResponseDto> CreateHostel(CreateHostelRequestDto createHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(createHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            Hostel hostel = new Hostel
            {
                HostelName = createHostelRequestDto.HostelName,
                HostelAddress = createHostelRequestDto.HostelAddress,
                HostelDescription = createHostelRequestDto.HostelDescription,
                AccountID = createHostelRequestDto.AccountID,
                Status = (int)HostelEnum.Available,
            };

            await _hostelRepository.CreateHostel(hostel);

            return new CreateHostelResponseDto { HostelID = hostel.HostelID};
        }

        public async Task<HostelDetailAdminView> GetHostelDetailAdminView(int hostelID)
        {
            var hostel = await _hostelRepository.GetHostelById(hostelID);
            return _mapper.Map<HostelDetailAdminView>(hostel);
        }

        public async Task<IEnumerable<HostelListResponseDto>> GetHostels()
        {
            var hostels = await _hostelRepository.GetAllHostels();
            return _mapper.Map<IEnumerable<HostelListResponseDto>>(hostels);
        }

        public async Task<IEnumerable<HostelsAdminView>> GetHostelsAdminView()
        {
            var hostels = await _hostelRepository.GetAllHostels();
            return _mapper.Map<IEnumerable<HostelsAdminView>>(hostels);
        }

        public async Task<IEnumerable<HostelListResponseDto>> GetHostelsByOwner(int onwerId)
        {
            var ownerAccount = await _accountRepository.GetAccountWithHostelById(onwerId);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }
            if (ownerAccount.Hostels == null)
            {
                return null;
            }

            var ownerHostels = await _hostelRepository.GetOwnerHostels(onwerId);
            return _mapper.Map<IEnumerable<HostelListResponseDto>>(ownerHostels);

        }


        public async Task UpdateHostel(UpdateHostelRequestDto updateHostelRequestDto)
        {
            var ownerAccount = await _accountRepository.GetAccountById(updateHostelRequestDto.AccountID);
            if (ownerAccount == null)
            {
                throw new ServiceException("Account not found with this ID");
            }

            var currentHostel = await _hostelRepository.GetHostelById(updateHostelRequestDto.HostelId);
            if (currentHostel == null)
            {
                throw new ServiceException("Hostel not found with this ID");
            }

            if (currentHostel.AccountID != updateHostelRequestDto.AccountID)
            {
                throw new ServiceException("The hostel does not belong to the specified account.");
            }

            currentHostel.HostelName = updateHostelRequestDto.HostelName;
            currentHostel.HostelDescription = updateHostelRequestDto.HostelDescription;
            currentHostel.HostelAddress = updateHostelRequestDto.HostelAddress;

            await _hostelRepository.UpdateHostel(currentHostel);
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

                    await _hostelRepository.UpdateHostel(currentHostel);
				}
				catch (Exception ex)
				{
					throw new ServiceException("Upload hostel image fail with error", ex);
				}
			}
		}
	}
}
