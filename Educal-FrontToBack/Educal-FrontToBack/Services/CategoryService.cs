using Educal_FrontToBack.Data;
using Educal_FrontToBack.Models;
using Educal_FrontToBack.Services.Interfaces;
using Educal_FrontToBack.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Educal_FrontToBack.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDBContext _context;
        public CategoryService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category category)
        {
            await _context.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<bool> ExistExceptByIdAsync(int id, string name)
        {
            return await _context.Categories.AnyAsync(m => m.Name == name && m.Id != id);
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.Include(m => m.Image).ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Categories.Where(m => !m.SoftDeleted).Include(m => m.Courses)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

        public async Task<SelectList> GetAllSelectedAsync()
        {
            var categories = await _context.Categories.Where(m => !m.SoftDeleted).ToListAsync();
            return new SelectList(categories, "Id", "Name");
        }

        public async Task<IEnumerable<Category>> GetAllWithAllDatasAsync()
        {
            return await _context.Categories.Include(m => m.Courses).ThenInclude(m => m.CoursesImages).ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Category> GetByIdWithCoursesAsync(int id)
        {
            return await _context.Categories.Include(m=>m.Courses).ThenInclude(m=>m.CoursesImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Categories.CountAsync();
        }

        public IEnumerable<CategoryCourseVM> GetMappedDatas(IEnumerable<Category> category)
        {
            return category.Select(m => new CategoryCourseVM()
            {
                Id = m.Id,
                CreatedDate = m.CreatedDate.ToString("dddd.MM.yyyy"),
                CategoryName = m.Name,
                CourseCount=m.Courses.Count()
            }); ;
        }
    }
}
