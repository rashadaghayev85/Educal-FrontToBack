using Educal_FrontToBack.Services.Interfaces;
using Educal_FrontToBack.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Educal_FrontToBack.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ICourseService _courseService;
        public HomeController(ICategoryService categoryService,
                              ICourseService courseService)
        {
            _categoryService = categoryService;
            _courseService = courseService;
        }
        public async Task<IActionResult> Index()
        {
            HomeVM model = new()
            {
                Categories = await _categoryService.GetAllWithAllDatasAsync(),
                Courses = await _courseService.GetAllWithAllDatasAsync(),
            };
            return View(model);
        }

      
    }
}
