using E_G_Lab06.Models.ViewModels;
using E_G_Lab06.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace E_G_Lab06.Controllers
{
    public class UserController : Controller
    {
       
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        public UserController(IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.ReadAllAsync();
            var userList = users
               .Select(u => new UserListVM
               {
                   Email = u.Email,
                   UserName = u.UserName,
                   NumberOfRoles = u.Roles.Count,
                   RoleNames = string.Join(",", u.Roles.ToArray())
               });
            return View(userList);
        }
        public async Task<IActionResult> AssignRole([Bind(Prefix = "Id")] string username)
        {
            var user = await _userRepository.ReadAsyncByUserName(username);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            var allRoles = _roleRepository.ReadAll().ToList();
            var allRoleNames = allRoles.Select(r => r.Name);
            var rolesUserDoNotHave = allRoleNames.Except(user.Roles);
            ViewData["User"] = user;
            return View(rolesUserDoNotHave);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(string userName, string roleName)
        {
            var user = await _userRepository.ReadAsyncByUserName(userName);
            if (user == null)
            {
                return RedirectToAction("Index");
            }
            
            await _userRepository.AssignRoleAsync(userName, roleName);
            return RedirectToAction("Index");
        }


    }
}
