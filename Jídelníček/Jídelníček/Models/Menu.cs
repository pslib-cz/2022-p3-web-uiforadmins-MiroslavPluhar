using System.ComponentModel.DataAnnotations;

namespace Jídelníček.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<Food> Foods { get; set; }
    }
}
