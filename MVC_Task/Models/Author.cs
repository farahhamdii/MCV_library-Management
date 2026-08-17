using MVC_Task.Validation;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task.Models
{
    public class Author
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Author name is required")]
        [StringLength(50, MinimumLength = 2,
            ErrorMessage = "Name must be between 2 and 50 characters")]
        [UniqueAuthorName(ErrorMessage = "Author name already exists")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Bio is required")]
        [StringLength(500, MinimumLength = 10,
            ErrorMessage = "Bio must be between 10 and 500 characters")]
        public string Bio { get; set; }

        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime DateOfBirth { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();
    }
}