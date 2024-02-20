using AdminPanel.Models;

namespace AdminPanel.Service.IService
{
    public interface ICustomerService
    {
        Task<ResponseDto> GetUser();
    }
}
