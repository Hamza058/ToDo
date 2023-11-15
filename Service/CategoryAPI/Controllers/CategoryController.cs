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

        [HttpGet("getList")]
        public IActionResult GetList()
        {
            return Ok(cm.TGetList());
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct(Category category)
        {
            try
            {
                cm.TAdd(category);

                var response = new ResponseDto()
                {
                    Result = category,
                    IsSuccess = true,
                    Message = "Success"
                };

                return Ok(response);
            }
            catch
            {
                var response = new ResponseDto()
                {
                    Result = null,
                    IsSuccess = false,
                    Message = "Faild"
                };

                return Ok(response);
            }
            
        }
    }
}
