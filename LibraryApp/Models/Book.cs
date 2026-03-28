using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryApp.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Title zaroori hai")]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Author zaroori hai")]
        [MaxLength(100)]
        public string Author { get; set; }

        [Required(ErrorMessage = "Category select karo")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        [Display(Name = "Category")]
        public Category? Category { get; set; }  // Navigation property

        [Required(ErrorMessage = "Price zaroori hai")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0, 99999, ErrorMessage = "Valid price likho")]
        public decimal Price { get; set; }

        [Display(Name = "Published Date")]
        [DataType(DataType.Date)]
        public DateTime PublishedDate { get; set; } = DateTime.Now;

        [MaxLength(500)]
        public string? Description { get; set; }

        [Display(Name = "Cover Image URL")]
        public string? CoverImageUrl { get; set; }

        [Display(Name = "Available")]
        public bool IsAvailable { get; set; } = true;

        [Display(Name = "Total Pages")]
        [Range(1, 10000)]
        public int Pages { get; set; }
    }
}
