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
        public IList<Food> Food { get; set; } = default!;

        public string FirstNameSort { get; set; }
        public string LastNameSort { get; set; }
        public string NameSort { get; set; }
        public string KindSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [TempData]
        public string Alert { get; set; }

        public async Task OnGetAsync(string sortOrder)
        {
            FirstNameSort = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            LastNameSort = String.IsNullOrEmpty(sortOrder) ? "lastname_desc" : "";
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            KindSort = String.IsNullOrEmpty(sortOrder) ? "kindname_desc" : "";

            IQueryable<User> usersIQ = from u in _context.Users
            select u;

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

            IQueryable<Food> foodsIQ = from f in _context.Foods
            select f;

            switch (sortOrder)
            {
                case "name_desc":
                    foodsIQ = foodsIQ.OrderByDescending(f => f.Name);
                    break;
                case "kindname_desc":
                    foodsIQ = foodsIQ.OrderByDescending(f => f.Kind);
                    break;
                default:
                    foodsIQ = foodsIQ.OrderBy(f => f.Name);
                    break;
            }

            Food = await foodsIQ.AsNoTracking().ToListAsync();
        }
    }
}