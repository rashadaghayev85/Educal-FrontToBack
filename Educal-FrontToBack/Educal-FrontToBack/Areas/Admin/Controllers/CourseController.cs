using Educal_FrontToBack.Helpers.Extensions;
using Educal_FrontToBack.Helpers;
using Microsoft.AspNetCore.Mvc;
using Educal_FrontToBack.Services.Interfaces;
using Educal_FrontToBack.ViewModels.Courses;
using Educal_FrontToBack.Models;

namespace Educal_FrontToBack.Areas.Admin.Controllers
{
    [Area("admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IWebHostEnvironment _env;
        public CourseController(ICourseService courseService,
                                  IWebHostEnvironment env,
                                  ICategoryService categoryService)
        {
            _courseService = courseService;
            _env = env;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int page = 1)
        {
            var courses = await _courseService.GetAllPaginateAsync(page, 4);

            var mappedDatas = _courseService.GetMappedDatas(courses);
            int totalPage = await GetPageCountAsync(4);

            Paginate<CourseVM> paginateDatas = new(mappedDatas, totalPage, page);

            return View(paginateDatas);
        }

        private async Task<int> GetPageCountAsync(int take)
        {
            int productCount = await _courseService.GetCountAsync();

            return (int)Math.Ceiling((decimal)productCount / take);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int? id)
        {
            if (id is null) return BadRequest();
            var existProduct = await _courseService.GetByIdWithCoursesImagesAsync((int)id);
            if (existProduct is null) return NotFound();
            var category = await _categoryService.GetByIdAsync(existProduct.CategoryId);

            List<CourseImageVM> images = new();
            foreach (var item in existProduct.CoursesImages)
            {
                images.Add(new CourseImageVM
                {
                    Image = item.Name,
                    IsMain = item.IsMain

                });
            }
            CourseDetailVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                Category = category.Name,
                Price = existProduct.Price,
                Images = images
            };
            return View(response);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseCreateVM request)
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            if (!ModelState.IsValid)
            {
                return View();

            }

            foreach (var item in request.Images)
            {
                if (!item.CheckFileSize(500))
                {
                    ModelState.AddModelError("Images", "Image size must be max 500 KB");
                    return View();
                }

                if (!item.CheckFileType("image/"))
                {
                    ModelState.AddModelError("Images", "File type must be only image");

                    return View();
                }
            }
            List<CourseImage> images = new();
            foreach (var item in request.Images)
            {
                string fileName = $"{Guid.NewGuid()}-{item.FileName}";
                string path = _env.GenerateFilePath("assets/images", fileName);
                await item.SaveFileToLocalAsync(path);
                images.Add(new CourseImage { Name = fileName });
            }

            images.FirstOrDefault().IsMain = true;
            Course product = new()
            {
                Name = request.Name,
                Description = request.Description,
                Color=request.Color,
                CategoryId = request.CategoryId,
                Price = decimal.Parse(request.Price.Replace(".", ",")),
                CoursesImages = images

            };

            await _courseService.CreateAsync(product);


            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var existProduct = await _courseService.GetByIdWithCoursesImagesAsync((int)id);
            if (existProduct is null) return NotFound();

            foreach (var item in existProduct.CoursesImages)
            {
                string path = _env.GenerateFilePath("assets/images", item.Name);

                path.DeleteFileFromLocal();
            }
            await _courseService.DeleteAsync(existProduct);
            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {


            if (id is null) return BadRequest();

            var existProduct = await _courseService.GetByIdWithCoursesImagesAsync((int)id);

            if (existProduct is null) return NotFound();



            ViewBag.categories = await _categoryService.GetAllSelectedAsync();

            List<CourseImageVM> images = new();

            foreach (var item in existProduct.CoursesImages)
            {
                images.Add(new CourseImageVM
                {
                    Id = item.Id,
                    Image = item.Name,
                    IsMain = item.IsMain
                });
            }

            CourseEditVM response = new()
            {
                Name = existProduct.Name,
                Description = existProduct.Description,
                // Price = existProduct.Price.ToString().Replace(",", "."),
                Images = images,
                CategoryId = existProduct.CategoryId,
            };





            return View(response);
            //  return View(new ProductEditVM { Name =existProduct.Name, Description =existProduct.Description,Images=images,Price=prod.Price.ToString() });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CourseEditVM request)
        {
            ViewBag.categories = await _categoryService.GetAllSelectedAsync();
            if (!ModelState.IsValid)
            {
                var product = await _courseService.GetByIdAsync((int)id);

                List<CourseImageVM> imagesss = new();

                foreach (var item in product.CoursesImages)
                {
                    imagesss.Add(new CourseImageVM
                    {
                        Image = item.Name,
                        IsMain = item.IsMain
                    });
                }

                return View(new CourseEditVM { Images = imagesss });

            }

            if (id == null) return BadRequest();
            var products = await _courseService.GetByIdWithCoursesImagesAsync((int)id);
            if (products == null) return NotFound();


            if (request.NewImages is not null)
            {

                List<CourseImage> images = new();
                foreach (var item in request.NewImages)
                {
                    string fileName = $"{Guid.NewGuid()}-{item.FileName}";
                    string path = _env.GenerateFilePath("assets/images", fileName);
                    await item.SaveFileToLocalAsync(path);
                    images.Add(new CourseImage { Name = fileName });
                }

                foreach (var item in request.NewImages)
                {


                    if (!item.CheckFileType("image/"))
                    {
                        ModelState.AddModelError("NewImages", "Input can accept only image format");
                        products.CoursesImages = images;
                        return View(request);

                    }
                    if (!item.CheckFileSize(500))
                    {
                        ModelState.AddModelError("NewImages", "Image size must be max 500 KB ");
                        products.CoursesImages = images;
                        return View(request);
                    }


                }
                foreach (var item in request.NewImages)
                {
                    string oldPath = _env.GenerateFilePath("assets/images", item.Name);
                    oldPath.DeleteFileFromLocal();
                    string fileName = Guid.NewGuid().ToString() + "-" + item.FileName;
                    string newPath = _env.GenerateFilePath("assets/images", fileName);

                    await item.SaveFileToLocalAsync(newPath);





                    products.CoursesImages.Add(new CourseImage { Name = fileName });





                    //if (request.Price is not null)
                    //{
                    //    product.Price = request.Price;
                    //}




                    //if (fileName is not null)
                    //{
                    //    products.ProductImages=images;
                    //}

                }

            }

            if (request.Name is not null)
            {
                products.Name = request.Name;
            }
            if (request.Description is not null)
            {
                products.Description = request.Description;
            }
            if (request.CategoryId != 0)
            {
                products.CategoryId = request.CategoryId;
            }
            if (request.Price is not null)
            {
                products.Price = decimal.Parse(request.Price);
            }

            //if (!ModelState.IsValid)
            //{
            //    return View();
            //}







            await _courseService.EditAsync();
            return RedirectToAction(nameof(Index));
        }
		public async Task<IActionResult> IsMain(int? id)
		{
			if (id is null) return BadRequest();
			var productImage = await _courseService.GetCourseImageByIdAsync((int)id);

			if (productImage is null) return NotFound();

			//var images=await _productService.GetProductByNameAsync(productName);

			//foreach (var item in images.ProductImages)
			//{
			//    item.IsMain = false;
			//}
			var productId = productImage.CourseId;

			var pro = await _courseService.GetByIdWithCoursesImagesAsync(productId);
			foreach (var item in pro.CoursesImages)
			{
				item.IsMain = false;
			}
			productImage.IsMain = true;



			await _courseService.EditAsync();
			return Redirect($"/admin/course/edit/{productId}");

		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ImageDelete(int? id)
		{
			if (id is null) return BadRequest();
			var productImage = await _courseService.GetCourseImageByIdAsync((int)id);

			if (productImage is null) return NotFound();

			//var images=await _productService.GetProductByNameAsync(productName);

			//foreach (var item in images.ProductImages)
			//{
			//    item.IsMain = false;
			//}


			string path = _env.GenerateFilePath("assets/images", productImage.Name);

			path.DeleteFileFromLocal();

			var productId = productImage.CourseId;

			var pro = await _courseService.GetByIdWithCoursesImagesAsync(productId);

			await _courseService.ImageDeleteAsync(productImage);
			return Redirect($"/admin/course/edit/{productId}");

		}

	}
}
