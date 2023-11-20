using Azure;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
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
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        ProductManager pm = new ProductManager(new EFProductDal());

        public ProductController(IProductService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();
            var response = await _service.GetProductsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize]
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
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProductCreate()
        {
            ViewBag.category = cm.TGetList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(Product product)
        {
            ResponseDto? response = await _service.CreateProductAsync(product);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(product);
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> ProductEdit(int id)
        {
            ViewBag.category = cm.TGetList();
            return View(pm.TGetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> ProductEdit(Product product)
        {
            ResponseDto? response = await _service.CreateProductAsync(product);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Product updated successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(product);
        }
    }
}
