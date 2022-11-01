using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Jídelníček.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IList<User> User { get; set; } = default!;

        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [TempData]
        public string Alert { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";

            CurrentFilter = searchString;

            IQueryable<User> usersIQ = from u in _context.Users select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                usersIQ = usersIQ.Where(s => s.FirstName.Contains(searchString) || s.LastName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "firstname_desc":
                    usersIQ = usersIQ.OrderByDescending(u => u.FirstName);
                    break;
                case "lastname_desc":
                    usersIQ = usersIQ.OrderByDescending(u => u.LastName);
                    break;
                default:
                    usersIQ = usersIQ.OrderBy(u => u.LastName);
                    break;
            }

            User = await usersIQ.AsNoTracking().ToListAsync();
        }
    }
}