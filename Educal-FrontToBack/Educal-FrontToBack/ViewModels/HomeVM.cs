using Educal_FrontToBack.Models;

namespace Educal_FrontToBack.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}
