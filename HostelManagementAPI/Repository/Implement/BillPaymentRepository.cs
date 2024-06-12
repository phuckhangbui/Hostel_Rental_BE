using BusinessObject.Models;
using DAO;
﻿using AutoMapper;
using DTOs.BillPayment;
using Repository.Interface;
using DTOs.Enum;
using DTOs.Contract;
using DTOs.Room;

namespace Repository.Implement
{
    public class BillPaymentRepository : IBillPaymentRepository
    {
        private readonly IMapper _mapper;

        public BillPaymentRepository(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task CreateBillPaymentMonthly(
            RoomDetailResponseDto hiredRoomDto,
            GetContractDto currentContractDto, 
            CreateBillPaymentRequestDto createBillPaymentRequestDto,
            DateTime billingMonth)
        {
            double totalAmount = (double)hiredRoomDto.RoomFee;
            var billPaymentDetails = new List<BillPaymentDetail>();
            var selectedServices = await RoomServiceDao.Instance.GetRoomServicesIsSelected((int)currentContractDto.RoomID);

            foreach (var service in selectedServices)
            {
                //Serivce fix price per month
                if (service.TypeService.Unit.Equals("Month"))
                {
                    totalAmount += service.Price ?? 0;

                    var billPaymentDetail = new BillPaymentDetail
                    {
                        RoomServiceID = service.RoomServiceId,
                        OldNumberService = 0,
                        NewNumberService = 0,
                        Quantity = 1,
                        ServiceTotalAmount = service.Price,
                    };

                    billPaymentDetails.Add(billPaymentDetail);
                }
                else
                {
                    //Get last bill payment
                    double oldNumberService = 0;
                    var lastBillPayment = await BillPaymentDao.Instance.GetLastBillPaymentDetail(service.RoomServiceId);
                    if (lastBillPayment != null)
                    {
                        oldNumberService = lastBillPayment.NewNumberService ?? 0;
                    }

                    //
                    var serviceReading = createBillPaymentRequestDto.ServiceReadings
                        .FirstOrDefault(sr => sr.RoomServiceId == service.RoomServiceId);
                    if (serviceReading == null)
                    {
                        throw new Exception($"No new number service value provided for RoomServiceId {service.RoomServiceId}");
                    }

                    //Calculate usage service => total price
                    double newNumberService = serviceReading.NewNumberService;
                    double usage = newNumberService - oldNumberService;
                    double serviceTotalAmount = usage * (service.Price ?? 0);

                    var billPaymentDetail = new BillPaymentDetail
                    {
                        RoomServiceID = service.RoomServiceId,
                        OldNumberService = oldNumberService,
                        NewNumberService = newNumberService,
                        Quantity = (int)usage,
                        ServiceTotalAmount = serviceTotalAmount
                    };

                    billPaymentDetails.Add(billPaymentDetail);
                    totalAmount += serviceTotalAmount;
                }
            }

            var billPayment = new BillPayment
            {
                ContractId = currentContractDto.ContractID,
                BillAmount = (double)hiredRoomDto.RoomFee,
                Month = billingMonth.Month,
                Year = billingMonth.Year,
                CreatedDate = DateTime.Now,
                TotalAmount = totalAmount,
                BillPaymentStatus = (int)BillPaymentStatus.Pending,
                BillType = createBillPaymentRequestDto.BillType,
                Details = billPaymentDetails
            };

            await BillPaymentDao.Instance.CreateAsync(billPayment);
        }

        public async Task<BillPaymentDto> GetCurrentMonthBillPayment(int contractId, int month, int year)
        {
            
            var currentBillPayment = await BillPaymentDao.Instance.GetCurrentBillPayment(contractId, month, year);
            if (currentBillPayment != null)
            {
                return _mapper.Map<BillPaymentDto>(currentBillPayment);
            }

            return null;
        }

        public async Task<BillPaymentDto> GetLastMonthBillPayment(int contractId, int roomId)
        {
            var lastBillPayment = await BillPaymentDao.Instance.GetLastBillPayment(contractId);
            var lastBillPaymentDto = _mapper.Map<BillPaymentDto>(lastBillPayment);

            var selectedServices = await RoomServiceDao.Instance.GetRoomServicesIsSelected(roomId);
            var billPaymentDetails = new List<BillPaymentDetail>();

            foreach (var service in selectedServices)
            {
                var lastBillPaymentDetail = await BillPaymentDao.Instance.GetLastBillPaymentDetail(service.RoomServiceId);
                if (lastBillPaymentDetail != null)
                {
                    billPaymentDetails.Add(lastBillPaymentDetail);
                }
            }

            var billPaymentDetailDtos = _mapper.Map<IEnumerable<BillPaymentDetailResponseDto>>(billPaymentDetails);
            lastBillPaymentDto.BillPaymentDetails = (List<BillPaymentDetailResponseDto>)billPaymentDetailDtos;

            return lastBillPaymentDto;
        }

        public async Task<BillPaymentDto> GetBillPaymentById(int id)
        {
            var billPayment = await BillPaymentDao.Instance.GetBillPayment(id);
            return _mapper.Map<BillPaymentDto>(billPayment);
        }

        public async Task<BillPaymentDto> GetBillPaymentByTnxRef(string tnxRef)
        {
            var billPayment = await BillPaymentDao.Instance.GetBillPaymentByTnxRef(tnxRef);
            return _mapper.Map<BillPaymentDto>(billPayment);
        }

        public async Task<BillPaymentDto> CreateBillPayment(BillPaymentDto billPaymentDto)
        {
            var billPayment = _mapper.Map<BillPayment>(billPaymentDto);

            await BillPaymentDao.Instance.CreateAsync(billPayment);

            billPaymentDto.BillPaymentID = billPayment.BillPaymentID;

            return billPaymentDto;
        }

        public async Task<BillPaymentDto> UpdateBillPayment(BillPaymentDto billPaymentDto)
        {
            var billPayment = _mapper.Map<BillPayment>(billPaymentDto);

            await BillPaymentDao.Instance.UpdateAsync(billPayment);

            return billPaymentDto;
        }

        public async Task<IEnumerable<BillPaymentDto>> GetBillPaymentsByContractId(int contractId)
        {
            var billPayments = await BillPaymentDao.Instance.GetBillPaymentByContractId(contractId);

            return _mapper.Map<IEnumerable<BillPaymentDto>>(billPayments);
        }

        public async Task<BillPaymentDto> GetBillPaymentDetail(int billPaymentId)
        {
            var billPayment = await BillPaymentDao.Instance.GetBillPayment(billPaymentId);
            var billPaymentDto = _mapper.Map<BillPaymentDto>(billPayment);

            var billPaymentDetail = await BillPaymentDao.Instance.GetBillPaymentDetail(billPaymentId);
            var billPaymentDetailDtos = _mapper.Map<IEnumerable<BillPaymentDetailResponseDto>>(billPaymentDetail);

            billPaymentDto.BillPaymentDetails = (List<BillPaymentDetailResponseDto>)billPaymentDetailDtos;

            return billPaymentDto;
        }
    }
}
