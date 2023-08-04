using Cooperchip.ItDeveloper.Mvc.Extensions.ViewComponents.EstadoPaciente;
using Microsoft.EntityFrameworkCore;

namespace Cooperchip.ItDeveloper.Mvc
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            //builder.Services.AddMvc().AddTagHelpersAsServices();
            //builder.Services.AddRazorPages().AddTagHelpersAsServices();

            // Add services to the container.
            builder.Services.AddControllersWithViews();


            builder.Services.AddDbContext<ITDeveloperDbContext>(options =>
                   options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Dashboard}/{id?}");

            app.Run();
        }
    }
}