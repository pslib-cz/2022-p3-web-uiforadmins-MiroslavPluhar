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
    public class CreateUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        [TempData]
        public string Alert { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            _context.Users.Add(User);
            await _context.SaveChangesAsync();

            Alert = "Položka byla úspěšně přidána!";

            return RedirectToPage("./Index");
        }
    }
}
