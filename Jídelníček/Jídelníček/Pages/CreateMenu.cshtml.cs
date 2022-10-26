using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace Jídelníček.Pages
{
    public class CreateMenuModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateMenuModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public int UserId { get; set; }

        public IList<Food> Foods { get; set; }
        public List<Food> Food { get; set; }

        public async Task OnGet(int id)
        {
            UserId = id;
            Foods = await _context.Foods.ToListAsync();
        }

        [BindProperty]
        public Menu Menu { get; set; }

        [TempData]
        public string Alert { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int id, int[] FoodIds)
        {
            UserId = id;
            Foods = await _context.Foods.ToListAsync();
            Food = new List<Food>();

            foreach (var foodid in FoodIds)
            {
                foreach(var food in Foods)
                {
                    if(food.FoodId == foodid)
                    {
                        Food.Add(food);
                    }
                }
            }

            Menu = new Menu { Name = Menu.Name, UserId = UserId, Foods = Food };

            _context.Menus.Add(Menu);
            await _context.SaveChangesAsync();

            Alert = "Položka byla úspěšně přidána!";

            return RedirectToPage("./Menu", new { id = UserId});
        }
    }
}
