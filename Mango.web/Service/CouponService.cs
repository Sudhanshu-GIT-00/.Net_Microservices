using Mango.web.Models;
using Mango.web.Service.IService;
using Mango.web.Services.Models;

namespace Mango.web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;
        public CouponService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto?> DeleteCouponsAsynnc(int id)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto?> GetAllCouponAsync()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDto?> GetCouponByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> GetAllCouponsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto?> DeleteCouponsAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
