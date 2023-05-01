using E_G_Lab06.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace E_G_Lab06.Services
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DbUserRepository(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser?> ReadAsyncByUserName(string username)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.UserName == username);
            if (user != null)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return user;
        }

        public async Task<IQueryable<ApplicationUser>> ReadAllAsync()
        {
            var users = _db.Users;
            // Read the roles for each user in the database
            foreach (var user in users)
            {
                user.Roles = await _userManager.GetRolesAsync(user);
            }
            return users;
        }
        public async Task<bool> AssignRoleAsync(string userName, string roleName)
        {
            var roleCheck = await _roleManager.RoleExistsAsync(roleName);
            if (!roleCheck)
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
            var user = await ReadAsyncByUserName(userName);
            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, roleName);
            }
            
            return true;
        }


    }
}
