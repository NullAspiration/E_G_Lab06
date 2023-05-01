using E_G_Lab06.Models.Entities;

namespace E_G_Lab06.Services
{
    public interface IUserRepository
    {
        Task<ApplicationUser?> ReadAsyncByUserName(string username);

        Task<IQueryable<ApplicationUser>> ReadAllAsync();

        Task<bool>AssignRoleAsync(string userName, string role);



    }
}
