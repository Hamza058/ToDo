using Azure;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductAPI.Models;
using System.Xml;
using System.Text.Json.Nodes;

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

        [Route("getList")]
        [HttpGet]
        public ResponseDto GetList()
        {
            List<ProductDto> products = new List<ProductDto>();
            foreach (var item in pm.TGetList())
            {
                var product = new ProductDto()
                {
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    Price = item.Price,
                    CategoryName = item.Category.CategoryName
                };
                products.Add(product);
            }
            try
            {
                _response.Result = products; ;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            
            return _response;
        }
        [Route("addProduct")]
        [HttpPost]
        public ResponseDto AddProduct([FromBody] Product product)
        {
            pm.TAdd(product);
            try
            {
                _response.Result = product;
                _response.Message = "Success";
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [Route("deleteProduct/{id}")]
        [HttpDelete]
		public ResponseDto DeleteProduct(int id)
		{
            try
            {
				pm.TDelete(pm.TGetById(id));
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
