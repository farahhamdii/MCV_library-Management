using MVC_Task.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task.Models
{
    public class Book
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Book title is required")]
        [StringLength(100, MinimumLength = 2,
            ErrorMessage = "Title must be between 2 and 100 characters")]
        [UniqueBook(
            ErrorMessage = "This book title already exists in this category")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Publication date is required")]
        public DateTime publicationDate { get; set; }

        [Required(ErrorMessage = "Please select an author")]
        public int AuthorId { get; set; }

        public Author Author { get; set; }

        [Required(ErrorMessage = "Please select a category")]
        public Category Category { get; set; }
    }
}