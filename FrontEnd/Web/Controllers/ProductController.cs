using Azure;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Service.IService;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Product>? list = new();
            var response = await _service.GetProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Product>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
