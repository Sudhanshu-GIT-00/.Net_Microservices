using AdminPanel.Models;
using AdminPanel.Service.IService;
using AdminPanel.Utility;

namespace AdminPanel.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IBaseService _baseService;

        public CustomerService(IBaseService baseService)
        {
            this._baseService = baseService;
        }
        public async Task<ResponseDto> GetUser()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/GetUsers"
            });
        }
    }
}
