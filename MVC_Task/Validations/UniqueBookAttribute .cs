using MVC_Task.Context;
using MVC_Task.Models;
using MVC_Task.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace MVC_Task.Validation
{
    public class UniqueBookAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(
            object? value,
            ValidationContext validationContext)
        {
            string? title = value as string;

            if (title is null)
            {
                return ValidationResult.Success;
            }

            BookVM? vm = validationContext.ObjectInstance as BookVM;

            if (vm is null)
            {
                return ValidationResult.Success;
            }

            LibraryContext db = new LibraryContext();

            bool exists = db.Books.Any(b =>
                b.Title == title &&
                b.Category == vm.Category &&
                b.Id != vm.Id
            );

            if (exists)
            {
                return new ValidationResult(
                    "This book already exists in this category."
                );
            }

            return ValidationResult.Success;
        }
    }
}