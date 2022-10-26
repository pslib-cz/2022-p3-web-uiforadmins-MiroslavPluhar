using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jídelníček.Pages
{
    public class MenuModel : PageModel
    {
        private readonly ILogger<MenuModel> _logger;
        private readonly ApplicationDbContext _context;

        public MenuModel(ILogger<MenuModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Menu> Menu { get; set; } = default!;

        public int UserId { get; set; }

        [TempData]
        public string Alert { get; set; }

        public async Task OnGetAsync(int id)
        {
            UserId = id;
            if (_context.Menus != null)
            {
                Menu = await _context.Menus.Where(m => m.UserId == id).ToListAsync();
            }
        }
    }
}
