using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using SportsMeeting.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Data
{
    public class DataSeeder
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<DataSeeder> _logger;

        public DataSeeder(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<DataSeeder> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public void SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
            SeedCategories();
        }

        public void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("User").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }

            if (!roleManager.RoleExistsAsync("Admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }

        public void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            if (userManager.Users.FirstOrDefault(u=>u.UserName == "admin") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = "admin@localhost";
                user.UserName = user.Email;
                IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;
                
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "Admin").Wait();
                }
            }

            if (userManager.Users.FirstOrDefault(u => u.UserName == "testuser") == null)
            {
                ApplicationUser user = new ApplicationUser();
                user.Email = "testuser1@localhost";
                user.UserName = user.Email;
                IdentityResult result = userManager.CreateAsync(user, "Test123!").Result;
                
                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "User").Wait();
                }
            }
        }

        public void SeedCategories()
        {
            if(_dbContext.Category.FirstOrDefault(c=>c.Name == "football") == null)
            {
                Category category = new Category();
                category.Name = "football";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "golf") == null)
            {
                Category category = new Category();
                category.Name = "golf";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "volleyball") == null)
            {
                Category category = new Category();
                category.Name = "volleyball";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "esport") == null)
            {
                Category category = new Category();
                category.Name = "esport";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "biliard") == null)
            {
                Category category = new Category();
                category.Name = "biliard";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "other") == null)
            {
                Category category = new Category();
                category.Name = "other";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }

            if (_dbContext.Category.FirstOrDefault(c => c.Name == "basketball") == null)
            {
                Category category = new Category();
                category.Name = "basketball";
                _dbContext.Category.Add(category);
                _dbContext.SaveChanges();
            }
        }

       
    }
}
