using System;
using System.Linq;
using System.Threading.Tasks;
using Homey.Common;
using Homey.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Homey.Data.Seeding
{
    internal class UsersToRolesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();

            await SeedUserToRoles(dbContext, userManager, roleManager);
        }

        private static async Task SeedUserToRoles(
            ApplicationDbContext dbContext,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("admin");
            var role = await roleManager.FindByNameAsync(GlobalConstants.AdminRoleName);

            var exists = dbContext.UserRoles.Any(x => x.UserId == user.Id && x.RoleId == role.Id);

            if (exists)
            {
                return;
            }

            dbContext.UserRoles.Add(new IdentityUserRole<int>
            {
                UserId = user.Id,
                RoleId = role.Id
            });

            await dbContext.SaveChangesAsync();
        }
    }
}
