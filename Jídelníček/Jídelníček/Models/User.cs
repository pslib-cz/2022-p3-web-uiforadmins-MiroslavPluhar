using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Jídelníček.Models
{
    public class User : IdentityUser<int>
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
