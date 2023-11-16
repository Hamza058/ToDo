using EntityLayer.Concrete;
using Web.Models;

namespace Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductsAsync();
        Task<ResponseDto?> CreateProductAsync(Product product);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
