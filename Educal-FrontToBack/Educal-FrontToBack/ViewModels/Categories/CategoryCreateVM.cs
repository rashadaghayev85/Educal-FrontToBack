using System.ComponentModel.DataAnnotations;

namespace Educal_FrontToBack.ViewModels.Categories
{
    public class CategoryCreateVM
    {
        [Required(ErrorMessage = "This input can't be empty")]
        [StringLength(20)]
        public string Name { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
