using System.ComponentModel.DataAnnotations;

namespace Jídelníček.Models
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public Kind Kind { get; set; }
        public ICollection<Menu> Menus { get; set; }
    }
}
