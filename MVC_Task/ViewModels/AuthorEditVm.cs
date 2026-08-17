using System.ComponentModel.DataAnnotations;

namespace MVC_Task.ViewModels
{
    public class AuthorEditVm
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Bio { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}
