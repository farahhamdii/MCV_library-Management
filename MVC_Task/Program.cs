using MVC_Task.Context;
using MVC_Task.Models;
using MVC_Task.Middlewares;
namespace MVC_Task
{
    public class Program
    {
        public static void Main(string[] args)
        {
          
            
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseMiddleware<RequestMonitoringMiddleware>();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.MapControllerRoute(
             name: "authors",
             pattern: "library/authors/{action}/{id?}",
             defaults: new { controller = "Author", action = "GetAll" }
             );

            app.MapControllerRoute(

             name: "books",
             pattern: "library/books/{action}/{id?}",
              defaults: new { controller = "Book", action = "GetAll" });

            app.Run();
        }
    }
}
