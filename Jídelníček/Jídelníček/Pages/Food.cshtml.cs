using Jídelníček.Data;
using Jídelníček.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Jídelníček.Pages
{
    public class FoodModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context;

        public FoodModel(ILogger<IndexModel> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IList<Food> Food { get; set; } = default!;

        public string NameSort { get; set; }
        public string KindSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        [TempData]
        public string Alert { get; set; }

        public async Task OnGetAsync(string sortOrder, string searchString)
        {
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            KindSort = String.IsNullOrEmpty(sortOrder) ? "kindname_desc" : "";

            CurrentFilter = searchString;

            IQueryable<Food> foodsIQ = from f in _context.Foods select f;

            if (!String.IsNullOrEmpty(searchString))
            {
                foodsIQ = foodsIQ.Where(s => s.Name.Contains(searchString));
            }

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
