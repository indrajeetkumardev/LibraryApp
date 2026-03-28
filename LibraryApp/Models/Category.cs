using System.ComponentModel.DataAnnotations;

namespace LibraryApp.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Category name zaroori hai")]
        [MaxLength(50)]
        [Display(Name = "Category Name")]
        public string Name { get; set; }

        [MaxLength(200)]
        public string Description { get; set; }

        // Navigation Property — ek category mein multiple books
        public List<Book> Books { get; set; } = new();
    }
}
