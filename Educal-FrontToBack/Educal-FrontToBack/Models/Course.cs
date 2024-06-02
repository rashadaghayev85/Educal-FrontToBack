using Educal_FrontToBack.ViewModels.Categories;

namespace Educal_FrontToBack.Models
{
    public class Course:BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public List<CourseImage> CoursesImages { get; set; }

       
    }
}
