using Microsoft.EntityFrameworkCore;
using MVC_Task.Models;
using System.Collections.Generic;

namespace MVC_Task.Context
{
    public class LibraryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-SHJSEL4\\SQLEXPRESS;Database=MVC_Task;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
    }
}