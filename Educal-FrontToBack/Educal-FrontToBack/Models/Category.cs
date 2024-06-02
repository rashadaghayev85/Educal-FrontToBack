namespace Educal_FrontToBack.Models
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Course> Courses { get; set; }
        public string ImageName {  get; set; }
        public CategoryImage Image { get; set; }
    }
}
