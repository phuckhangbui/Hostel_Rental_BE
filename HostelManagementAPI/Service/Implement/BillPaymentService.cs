using BusinessObject.Models;
﻿using DTOs;
using DTOs.BillPayment;
using DTOs.Enum;
using Repository.Interface;
using Service.Exceptions;
using Service.Interface;

namespace Service.Implement
{
    public class BillPaymentService : IBillPaymentService
    {
        private readonly IBillPaymentRepository _billPaymentRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IRoomRepository _roomRepository;

        public BillPaymentService(
            IBillPaymentRepository billPaymentRepository, 
            IContractRepository contractRepository,
            IRoomRepository roomRepository)
        {
            _billPaymentRepository = billPaymentRepository;
            _contractRepository = contractRepository;
            _roomRepository = roomRepository;
        }

        public async Task CreateBillPaymentMonthly(CreateBillPaymentRequestDto createBillPaymentRequestDto)
        {
            var contractId = createBillPaymentRequestDto.ContractId;

            var currentContract = await _contractRepository.GetContractById(contractId);
            if (currentContract == null)
            {
                throw new ServiceException("Contract not found with this ID");
            }
            else
            {
                var currentDate = DateTime.Now;
                var year = currentDate.Year;
                var month = currentDate.Month;

                //if (currentDate < currentContract.DateStart || currentDate > currentContract.DateEnd)
                //{
                //    throw new ServiceException("Contract is not active yet");
                //}

                int monthsSinceStart = ((currentDate.Year - currentContract.DateStart.Value.Year) * 12) +
                    currentDate.Month - currentContract.DateStart.Value.Month;
                var billingMonth = currentContract.DateStart.Value.AddMonths(monthsSinceStart);

                //Check bill exist or not
                var existingBillPayment = await _billPaymentRepository.GetCurrentMonthBillPayment(contractId, month, year);
                if (existingBillPayment != null)
                {
                    throw new ServiceException("A bill for this month already exists.");
                }
                else
                {
                    var hiredRoom = await _roomRepository.GetRoomById((int)currentContract.RoomID);
                    if (hiredRoom != null)
                    {
                        double totalAmount = (double)hiredRoom.RoomFee;
                        var billPaymentDetails = new List<BillPaymentDetail>();
                        var selectedServices = await _roomRepository.GetRoomServicesIsSelected((int)currentContract.RoomID);

                        foreach (var service in selectedServices)
                        {
                            //Serivce fix price per month
                            if (service.TypeService.Unit.Equals("Month")) {
                                totalAmount += service.Price ?? 0;

                                var billPaymentDetail = new BillPaymentDetail
                                {
                                    RoomServiceID = service.RoomServiceId,
                                    OldNumberService = 0,
                                    NewNumberService = 0,
                                    Quantity = 1,
                                    ServiceTotalAmount = service.Price ?? 0
                                };

                                billPaymentDetails.Add(billPaymentDetail);
                            }
                            else
                            {
                                //Get last bill payment
                                double oldNumberService = 0;
                                var lastBillPayment = await _billPaymentRepository.GetLastBillPaymentDetail(service.RoomServiceId);
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
                            ContractId = currentContract.ContractID,
                            BillAmount = (double)hiredRoom.RoomFee,
                            Month = billingMonth.Month,
                            Year = billingMonth.Year,
                            CreatedDate = DateTime.Now,
                            TotalAmount = totalAmount,
                            BillPaymentStatus = (int)BillPaymentStatus.Pending,
                            BillType = createBillPaymentRequestDto.BillType,
                            Details = billPaymentDetails
                        };

                        await _billPaymentRepository.CreateBillPaymentMonthly(billPayment);
                    }
                }
            }
        }

        public async Task<BillPaymentDto> CreateDepositPayment(DepositRoomInputDto depositRoomInputDto, int accountId)
        {
            var contract = await _contractRepository.GetContractById(depositRoomInputDto.ContractId);



            var billpayment = new BillPaymentDto
            {
                ContractId = contract.ContractID,
                BillAmount = contract.DepositFee,
                TotalAmount = contract.DepositFee,
                BillType = (int)BillType.Deposit,
                BillPaymentStatus = (int)BillPaymentStatus.Pending,
                CreatedDate = DateTime.Now,
                TnxRef = DateTime.Now.Ticks.ToString(),
            };

            billpayment = await _billPaymentRepository.CreateBillPayment(billpayment);

            return billpayment;
        }

        public async Task<BillPaymentDto> ConfirmDepositTransaction(VnPayReturnUrlDto vnPayReturnUrlDto)
        {
            var billPayment = await _billPaymentRepository.GetBillPaymentByTnxRef(vnPayReturnUrlDto.TnxRef);

            if (billPayment == null)
            {
                throw new ServiceException("No bill match");
            }


            if (vnPayReturnUrlDto == null)
            {
                throw new ServiceException("No transaction match");
            }

            if (billPayment.BillPaymentStatus != (int)BillPaymentStatus.Pending)
            {
                throw new ServiceException("Billpayment has been paid");
            }

            billPayment.BillPaymentStatus = (int)BillPaymentStatus.Paid;

            await _billPaymentRepository.UpdateBillPayment(billPayment);

            return billPayment;
        }
    }
}
