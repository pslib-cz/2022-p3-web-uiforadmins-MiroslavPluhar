using Jídelníček.Data;
using Jídelníček.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Jídelníček.Pages
{
    public class CreateFoodModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateFoodModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Food Food { get; set; }

        [TempData]
        public string Alert { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Foods.Add(Food);
            await _context.SaveChangesAsync();

            Alert = "Položka byla úspěšně přidána!";

            return RedirectToPage("./Food");
        }
    }
}
