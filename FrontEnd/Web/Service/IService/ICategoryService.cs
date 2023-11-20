using EntityLayer.Concrete;
using Web.Models;

namespace Web.Service.IService
{
    public interface ICategoryService
    {
        Task<ResponseDto?> GetCategoriesAsync();
        Task<ResponseDto?> CreateCategoryAsync(Category category);
        Task<ResponseDto?> DeleteCategoryAsync(int id);
    }
}
