using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jídelníček.Pages
{
    public class DetailsMenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsMenuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Menu Menu { get; set; }

        public List<Food> Foods { get; set; }

        public int UserId { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FirstOrDefaultAsync(f => f.MenuId == id);
            UserId = menu.UserId;
            Foods = await _context.Menus.Where(m => m.MenuId == id).SelectMany(f => f.Foods).ToListAsync();
            if (menu == null)
            {
                return NotFound();
            }
            else
            {
                Menu = menu;
            }
            return Page();
        }
    }
}
