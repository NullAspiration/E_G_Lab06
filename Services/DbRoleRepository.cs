using Microsoft.AspNetCore.Identity;

namespace E_G_Lab06.Services
{
    public class DbRoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public DbRoleRepository(ApplicationDbContext db)
        {
            _db = db;
        }


        public IQueryable<IdentityRole> ReadAll()
        {
            return _db.Roles;
        }

    }
}
