using AutoMapper;
using BusinessObject.Models;
using DAO;
using DTOs.BillPayment;
using Repository.Interface;

namespace Repository.Implement
{
    public class BillPaymentRepository : IBillPaymentRepository
    {
        private readonly IMapper _mapper;

        public BillPaymentRepository(IMapper mapper)
        {
            _mapper = mapper;
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
    }
}
