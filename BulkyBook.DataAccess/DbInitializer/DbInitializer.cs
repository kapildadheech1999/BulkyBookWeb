using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Modals;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.DbInitializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext context;

        public DbInitializer(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext context)
        {
            _userManager = userManager;
            this.context = context;
            _roleManager = roleManager;
        }
        public void Initialize()
        {
            // migrations if they are not applied
            try
            {
                if(context.Database.GetPendingMigrations().Count() > 0)
                {
                    context.Database.Migrate(); 
                }
            }
            catch(Exception e)
            {

            }

            //create roles if they are not created
            if (!_roleManager.RoleExistsAsync(SD.Role_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_Employee)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Indi)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(SD.Role_User_Comp)).GetAwaiter().GetResult();

                //if roles are not created, then we will create admin user as well

                _userManager.CreateAsync(new ApplicationUser()
                {
                    UserName = "admin@bulky.com",
                    Email = "admin@bulky.com",
                    Name = "test name",
                    PhoneNumber = "1112223334",
                    StreetAddress = "test 123 Ave",
                    State = "IL",
                    PostalCode ="23422",
                    City = "Chicago"

                }, "Kapil161@").GetAwaiter().GetResult();
                ApplicationUser user = context.ApplicationUsers.FirstOrDefault(u => u.Email == "admin@bulky.com");
                _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            }
            return;
        }
    }
}
