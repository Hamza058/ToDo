using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using Web.Service.IService;

namespace Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;
        CategoryManager cm = new CategoryManager(new EFCategoryDal());
        ProductManager pm = new ProductManager(new EFProductDal());

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            List<Category>? list = new();
            var response = await _service.GetCategoriesAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<Category>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        [Authorize]
        public async Task<IActionResult> CategoryDelete(int id)
        {
            ResponseDto? response = await _service.DeleteCategoryAsync(id);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category deleted successfully";
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
        public async Task<IActionResult> CategoryCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CategoryCreate(Category category)
        {
            ResponseDto? response = await _service.CreateCategoryAsync(category);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Category created successfully";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                TempData["error"] = response?.Message;
            }

            return View(category);
        }
    }
}
