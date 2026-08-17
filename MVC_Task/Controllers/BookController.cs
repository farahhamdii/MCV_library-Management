using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Task.Context;
using MVC_Task.Models;
using MVC_Task.ViewModels;

namespace MVC_Task.Controllers
{
    [Route("books")]
    public class BookController : Controller
    {
        LibraryContext db;

        public BookController()
        {
            db = new LibraryContext();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetAll()
        {
            var books = db.Books.Include(b => b.Author).ToList();
            return View(books);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            var book = db.Books
                .Include(b => b.Author)
                .FirstOrDefault(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        [HttpGet]
        [Route("add")]
        public IActionResult Add()
        {
            BookVM vm = new BookVM();
            vm.Authors = db.Authors.ToList();

            return View(vm);
        }

        [HttpPost]
        [Route("add")]
        public IActionResult Add(BookVM vm)
        {
            if (ModelState.IsValid)
            {
                Book book = new Book();

                book.Title = vm.Title;
                book.publicationDate = vm.PublicationDate;
                book.AuthorId = vm.AuthorId;
                book.Category = vm.Category;

                db.Books.Add(book);
                db.SaveChanges();

                return RedirectToAction(nameof(GetAll));
            }

            vm.Authors = db.Authors.ToList();

            return View(vm);
        }

        [HttpGet]
        [Route("edit/{id:int}")]
        public IActionResult Edit(int id)
        {
            Book? book = db.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            BookVM vm = new BookVM()
            {
                Id = book.Id,
                Title = book.Title,
                PublicationDate = book.publicationDate,
                AuthorId = book.AuthorId,
                Category = book.Category,
                Authors = db.Authors.ToList()
            };

            return View(vm);
        }

        [HttpPost]
        [Route("edit/{id:int}")]
        public IActionResult Edit(int id, BookVM vm)
        {
            if (ModelState.IsValid)
            {
                Book? book = db.Books.Find(id);

                if (book == null)
                {
                    return NotFound();
                }

                book.Title = vm.Title;
                book.publicationDate = vm.PublicationDate;
                book.AuthorId = vm.AuthorId;
                book.Category = vm.Category;

                db.SaveChanges();

                return RedirectToAction(nameof(GetAll));
            }

            vm.Authors = db.Authors.ToList();

            return View(vm);
        }

        [HttpGet]
        [Route("delete/{id:int}")]
        public IActionResult Delete(int id)
        {
            var book = db.Books.Find(id);

            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            db.SaveChanges();

            return RedirectToAction(nameof(GetAll));
        }
    }
}