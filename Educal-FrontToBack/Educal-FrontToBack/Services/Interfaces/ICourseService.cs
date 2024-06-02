using Educal_FrontToBack.Models;
using Educal_FrontToBack.ViewModels.Categories;
using Educal_FrontToBack.ViewModels.Courses;

namespace Educal_FrontToBack.Services.Interfaces
{
    public interface ICourseService
    {
        Task<IEnumerable<Course>> GetAllAsync();
        Task<IEnumerable<Course>> GetAllWithAllDatasAsync();
        Task<IEnumerable<Course>> GetAllPaginateAsync(int page, int take);
        IEnumerable<CourseVM> GetMappedDatas(IEnumerable<Course> course);
        Task<int> GetCountAsync();
        Task<bool> ExistAsync(string name);
        Task CreateAsync(Course course);
        Task<Course> GetByIdAsync(int id);
        Task DeleteAsync(Course course);
        Task EditAsync();
        Task<Course> GetByIdWithCoursesImagesAsync(int id);
		Task<CourseImage> GetCourseImageByIdAsync(int id);
		Task ImageDeleteAsync(CourseImage image);
	}
}
