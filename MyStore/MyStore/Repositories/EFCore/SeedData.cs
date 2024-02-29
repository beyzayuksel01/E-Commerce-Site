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

                await userManager.AddToRoleAsync(user, "Admin");
            }


        }
    }
}