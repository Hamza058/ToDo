using Azure;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Newtonsoft.Json;
using System.Collections.Generic;
using Web.Models;
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

		public async Task<IActionResult> ProductDelete(int id)
		{
			ResponseDto? response = await _service.DeleteProductAsync(id);

			if (response != null && response.IsSuccess)
			{
				TempData["success"] = "Product deleted successfully";
				return RedirectToAction(nameof(Index));
			}

			else
			{
				TempData["error"] = response?.Message;
			}

			return NotFound();
		}
	}
}
