using Mango.web.Models;
using Mango.web.Services.Models;

namespace Mango.web.Service.IService
{
    public interface IProductService
    {
        //Task<ResponseDto?> GetProductAsync(string ProductCode);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductsAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductsAsync(int id);
    }
}