using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Task.Context;
using MVC_Task.Models;
using MVC_Task.ViewModels;

namespace MVC_Task.Controllers
{
    [Route("authors")]
    public class AuthorController : Controller
    {
        LibraryContext db;

        public AuthorController()
        {
            db = new LibraryContext();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var authors = db.Authors.ToList();
            return View(authors);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Details(int id)
        {
            var author = db.Authors
                .Include(a => a.Books)
                .FirstOrDefault(a => a.Id == id);

            if (author == null)
            {
                return NotFound();
            }

            AuthorDetailsVM authorVM = new AuthorDetailsVM
            {
                Name = author.Name,
                Bio = author.Bio,
                DateOfBirth = author.DateOfBirth,
                BooksCount = author.Books.Count,
                Books = author.Books
            };

            return View(authorVM);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(Author author)
        {
            if (ModelState.IsValid)
            {
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction(nameof(GetAll));
            }

            return View(author);
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            Author? author = db.Authors.Find(id);

            if (author == null)
            {
                return NotFound();
            }

            AuthorEditVm vm = new AuthorEditVm
            {
                Id = author.Id,
                Name = author.Name,
                Bio = author.Bio,
                DateOfBirth = author.DateOfBirth
            };

            return View(vm);
        }

        [HttpPost]
        [Route("edit/{id:int}")]
        public IActionResult Edit(int id, AuthorEditVm vm)
        {
            if (ModelState.IsValid)
            {
                Author? author = db.Authors.Find(id);

                if (author == null)
                    return NotFound();

                author.Name = vm.Name;
                author.Bio = vm.Bio;
                author.DateOfBirth = vm.DateOfBirth;

                db.SaveChanges();

                return RedirectToAction(nameof(GetAll));
            }

            return View(vm);
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var author = db.Authors.FirstOrDefault(a => a.Id == id);

            if (author != null)
            {
                db.Authors.Remove(author);
                db.SaveChanges();
            }

            return RedirectToAction(nameof(GetAll));
        }
    }
}