using E_G_Lab06.Models.Entities;
using Microsoft.AspNetCore.Identity;

namespace E_G_Lab06.Services
{
    public interface IRoleRepository
    {
        IQueryable<IdentityRole> ReadAll();
        
    }
}
