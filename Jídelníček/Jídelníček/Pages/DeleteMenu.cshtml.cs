using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jídelníček.Pages
{
    public class DeleteMenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteMenuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Menu Menu { get; set; }

        public int UserId { get; set; }

        public List<Food> Foods { get; set; }

        [TempData]
        public string Alert { get; set; }

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }
            var menu = await _context.Menus.FindAsync(id);

            UserId = menu.UserId;

            if (menu != null)
            {
                Menu = menu;
                _context.Menus.Remove(Menu);
                await _context.SaveChangesAsync();
            }

            Alert = "Položka byla úspěšně odstraněna!";

            return RedirectToPage("./Menu", new { id = UserId });
        }
    }
}
