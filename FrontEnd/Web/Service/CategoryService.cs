using EntityLayer.Concrete;
using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseService _baseService;

        public CategoryService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> CreateCategoryAsync(Category category)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = category,
                Url = SD.CategoryAPIBase + "/api/Category/addCategory"
            });
        }

        public async Task<ResponseDto?> DeleteCategoryAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.CategoryAPIBase + "/api/Category/deleteCategory/" + id
            });
        }

        public async Task<ResponseDto?> EditCategoryAsync(Category category)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = category,
                Url = SD.CategoryAPIBase + "/api/Category/editCategory"
            });
        }

        public async Task<ResponseDto?> GetCategoriesAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.CategoryAPIBase + "/api/Category/getList"
            });
        }
    }
}
