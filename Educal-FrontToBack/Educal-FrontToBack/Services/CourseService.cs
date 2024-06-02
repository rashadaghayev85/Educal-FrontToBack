using Educal_FrontToBack.Data;
using Educal_FrontToBack.Models;
using Educal_FrontToBack.Services.Interfaces;
using Educal_FrontToBack.ViewModels.Categories;
using Educal_FrontToBack.ViewModels.Courses;
using Microsoft.EntityFrameworkCore;

namespace Educal_FrontToBack.Services
{
    public class CourseService : ICourseService
    {
        private readonly AppDBContext _context;
        public CourseService(AppDBContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Course course)
        {
            await _context.AddAsync(course);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Course course)
        {
            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistAsync(string name)
        {
            return await _context.Courses.AnyAsync(m => m.Name.Trim() == name.Trim());
        }

        public async Task<IEnumerable<Course>> GetAllAsync()
        {
            return await _context.Courses.Include(m => m.CoursesImages).Include(m => m.Category).ToListAsync();
        }

        public async Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take)
        {
            return await _context.Courses.Where(m => !m.SoftDeleted).Include(m => m.CoursesImages)
                                          .Skip((page - 1) * take)
                                          .Take(take)
                                          .ToListAsync();
        }

       

        public async Task<Course> GetByIdAsync(int id)
        {
            return await _context.Courses.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<Course> GetByIdWithCoursesImagesAsync(int id)
        {
            return await _context.Courses.Include(m => m.CoursesImages).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<int> GetCountAsync()
        {
            return await _context.Courses.CountAsync();
        }

		public async Task<CourseImage> GetCourseImageByIdAsync(int id)
		{
			return await _context.CourseImages.FirstOrDefaultAsync(m => m.Id == id);
		}

		public  IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> course)
        {
            return course.Select(m => new CourseVM()
            {
                Id = m.Id,
                Name = m.Name,
               // CategoryName = m.Category.Name,
                Price = m.Price,
                Description = m.Description,
                Color = m.Color,
                MainImage = m.CoursesImages.FirstOrDefault(m=>m.IsMain).Name
            }) ;;
        }

		public async Task ImageDeleteAsync(CourseImage image)
		{
			_context.CourseImages.Remove(image);
			await _context.SaveChangesAsync();
		}

        public async Task<IEnumerable<Course>> GetAllWithAllDatasAsync()
        {
            return await _context.Courses.Include(m => m.CoursesImages).ToListAsync();
        }
    }
}
