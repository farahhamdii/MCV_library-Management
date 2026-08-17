using MVC_Task.Context;
using MVC_Task.Models;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task.Validation
{
    public class UniqueAuthorNameAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            string? name = value as string;

            if (name is null)
            {
                return ValidationResult.Success;
            }

            Author? author = validationContext.ObjectInstance as Author;

            LibraryContext db = new LibraryContext();

            bool exists = db.Authors.Any(a =>
                a.Name == name &&
                a.Id != author!.Id
            );

            if (exists)
            {
                return new ValidationResult(
                    "Author name already exists.");
            }

            return ValidationResult.Success;
        }
    }
}