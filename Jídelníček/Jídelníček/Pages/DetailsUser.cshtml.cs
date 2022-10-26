using Jídelníček.Data;
using Jídelníček.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace Jídelníček.Pages
{
    public class DetailsUserModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsUserModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User User { get; set; }

        public List<Menu> Menus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FirstOrDefaultAsync(m => m.UserId == id);
            Menus = await _context.Menus.Where(m => m.UserId == id).ToListAsync();
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                User = user;
            }
            return Page();
        }
    }
}
