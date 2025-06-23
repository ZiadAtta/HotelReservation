using Core.Enum;
using HotelReservation.Core.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Data;


namespace HotelReservation.Infrastructure.Data
{
    public class DbSeeder
    {
        public static async Task SeedDataAsync(IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            // Apply pending migrations (Optional: skip for production)
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            if ((await dbContext.Database.GetPendingMigrationsAsync()).Any())
            {
                await dbContext.Database.MigrateAsync();
            }

            var logger = scope.ServiceProvider.GetRequiredService<ILogger<DbSeeder>>();
            try
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                // Step 1: Ensure Admin role exists
                var adminRoleName = UserType.Admin.ToString();
                if (!await roleManager.RoleExistsAsync(adminRoleName))
                {
                    var roleResult = await roleManager.CreateAsync(new IdentityRole(adminRoleName));
                    if (!roleResult.Succeeded)
                    {
                        var roleErrors = roleResult.Errors.Select(e => e.Description);
                        logger.LogError($"Failed to create admin role. Errors: {string.Join(", ", roleErrors)}");
                        return;
                    }
                    logger.LogInformation("Admin role is created");
                }

                // Step 2: Create Admin user if not exists
                var adminEmail = "admin@example.com";
                var existingUser = await userManager.FindByEmailAsync(adminEmail);
                if (existingUser == null)
                {
                    var user = new ApplicationUser
                    {
                        Name = "Admin",
                        UserName = adminEmail,
                        Email = adminEmail,
                        EmailConfirmed = true,
                        SecurityStamp = Guid.NewGuid().ToString()
                    };

                    var createUserResult = await userManager.CreateAsync(user, "Admin@123");

                    if (!createUserResult.Succeeded)
                    {
                        var errors = createUserResult.Errors.Select(e => e.Description);
                        logger.LogError($"Failed to create admin user. Errors: {string.Join(", ", errors)}");
                        return;
                    }

                    // Step 3: Assign Admin role to user
                    var addToRoleResult = await userManager.AddToRoleAsync(user, adminRoleName);
                    if (!addToRoleResult.Succeeded)
                    {
                        var errors = addToRoleResult.Errors.Select(e => e.Description);
                        logger.LogError($"Failed to assign admin role. Errors: {string.Join(", ", errors)}");
                        return;
                    }

                    logger.LogInformation("Admin user is created and assigned to role");
                }
                else
                {
                    logger.LogInformation("Admin user already exists");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

    }
}
