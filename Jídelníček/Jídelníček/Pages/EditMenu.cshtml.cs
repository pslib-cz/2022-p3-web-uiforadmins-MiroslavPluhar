using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using System.Reflection.Metadata;

namespace Jídelníček.Pages
{
    public class EditMenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditMenuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Menu Menu { get; set; } = default!;

        [TempData]
        public string Alert { get; set; }

        public List<Food> Foods1 { get; set; }

        public List<Food> Foods2 { get; set; }

        public List<Food> Food { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Menus == null)
            {
                return NotFound();
            }

            var menu = await _context.Menus.FirstOrDefaultAsync(f => f.MenuId == id);

            Foods1 = await _context.Foods.ToListAsync();
            Foods2 = await _context.Menus.Where(m => m.MenuId == id).SelectMany(f => f.Foods).ToListAsync();

            if (menu == null)
            {
                return NotFound();
            }
            Menu = menu;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] FoodIds)
        {
            Foods1 = await _context.Foods.ToListAsync();
            Food = new List<Food>();

            foreach (var foodid in FoodIds)
            {
                foreach (var food in Foods1)
                {
                    if (food.FoodId == foodid)
                    {
                        Food.Add(food);
                    }
                }
            }

            var menu = await _context.Menus.FirstOrDefaultAsync(m => m.MenuId == Menu.MenuId);

            menu.Name = Menu.Name;
            menu.Foods = Food;

            _context.Menus.Attach(menu);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(Menu.MenuId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            Alert = "Položka byla úspěšně upravena!";

            return RedirectToPage("./Menu", new { id = Menu.UserId });
        }

        private bool MenuExists(int id)
        {
            return _context.Menus.Any(e => e.MenuId == id);
        }
    }
}
