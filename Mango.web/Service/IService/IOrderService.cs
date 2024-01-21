using Mango.web.Models;
using Mango.web.Services.Models;

namespace Mango.web.Service.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> CreateOrder(CartDto cartDto);
    }
}