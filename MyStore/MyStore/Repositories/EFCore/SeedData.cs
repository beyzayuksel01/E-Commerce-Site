using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore.Repositories.EFCore
{
    public class SeedData
    {
        private const string adminUser = "admin";
        private const string adminPassword = "admin123";

        public static async void InitializeDataAsync(IApplicationBuilder app)
        {
            var context = app.ApplicationServices.CreateAsyncScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();

            if (context.Database.GetAppliedMigrations().Any())
            {
                context.Database.Migrate();
            }

            var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<AppRole>>();

            // Ensure Admin role exists
            var adminRole = await roleManager.FindByNameAsync("Admin");

            if (adminRole == null)
            {
                await roleManager.CreateAsync(new AppRole { Name = "Admin" });
            }

            var user = await userManager.FindByNameAsync(adminUser);

            if (user == null)
            {
                user = new AppUser
                {
                    FullName = "Beyza Yuksel",
                    UserName = adminUser,
                    Email = "admin@beyzayuksel.com",
                    PhoneNumber = "123123"
                };

                await userManager.CreateAsync(user, adminPassword);

                // Assign the Admin role to the user
                await userManager.AddToRoleAsync(user, "Admin");
            }

            //if (!context.Products.Any())
            //{
            //    context.Products.AddRange(
            //        new Product { Name = "Monitor", Price = 30000, Quantity = 10, CategoryId = 1, BrandId = 6 , Image = "/img/monitor.jfif" },
            //        new Product { Name = "Dining Table", Price = 20000, Quantity = 20, CategoryId = 2 , BrandId = 7 , Image = "/img/dining_table.jfif" },
            //        new Product { Name = "Eyeshadow Palette", Price = 300, Quantity = 30, CategoryId = 3 , BrandId = 8 , Image = "/img/eyeshadow_palette.jfif" }
            //    );
            //    context.SaveChanges();
            //}

        }
    }
}