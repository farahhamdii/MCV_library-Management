using MVC_Task.Models;

namespace MVC_Task.ViewModels
{
    public class AuthorDetailsVM
    {
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int BooksCount { get; set; }

        public List<Book> Books { get; set; } = new();
    }
}