using BusinessLayer.Concrete;
using CategoryAPI.Models;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CategoryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        private readonly ResponseDto _response;

        public CategoryController()
        {
            _response = new ResponseDto();
        }
        [Route("getList")]
        [HttpGet]
        public ResponseDto GetList()
        {
            try
            {
                _response.Result = cm.TGetList();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }
        [Route("addCategory")]
        [HttpPost]
        public ResponseDto AddCategory(Category category)
        {
            try
            {
                cm.TAdd(category);
                _response.Result = category;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }
        [Route("editCategory")]
        [HttpPut]
        public ResponseDto EditCategory(Category category)
        {
            try
            {
                var value=cm.TGetById(category.CategoryId);
                value.CategoryName = category.CategoryName;
                cm.TUpdate(value);
                _response.Result = category;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;

            }
            return _response;
        }
        [Route("deleteCategory/{id}")]
        [HttpDelete]
		public ResponseDto DeleteCategory(int id)
		{
			try
			{
				cm.TDelete(cm.TGetById(id));
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;

			}
			return _response;
		}
	}
}
