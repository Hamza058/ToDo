using Azure;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;

namespace ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ProductManager pm = new ProductManager(new EFProductDal());
        private readonly ResponseDto _response;
        public ProductController()
        {
            _response = new ResponseDto();
        }

        [HttpGet("getList")]
        public ResponseDto GetList()
        {
            try
            {
                _response.Result = pm.TGetList();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            
            return _response;
        }

        [HttpPost("addProduct")]
        public ResponseDto AddProduct([FromBody] Product product)
        {
            pm.TAdd(product);

            var response = new ResponseDto()
            {
                Result = product,
                IsSuccess = true,
                Message = "Success"
            };
            
            return response;
        }
    }
}
