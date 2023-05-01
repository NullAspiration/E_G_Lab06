using Microsoft.AspNetCore.Identity;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace E_G_Lab06.Models.Entities
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string LastName { get; set; } = string.Empty;

        [NotMapped]
        public ICollection<string> Roles { get; set; }
        = new List<string>();

        public bool HasRole(string roleName)
        {
            return Roles.Any(r => r == roleName);
        }


    }
}
