using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Clothes
{
    public class IndexModel : PageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public IndexModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IList<Clothing> Clothing { get;set; }
        public ClothingData ClothingD { get; set; }
        public int ClothingID { get; set; }
        public int CategoryID { get; set; }


        public async Task OnGetAsync(int? id, int? categoryID)
        {
            ClothingD = new ClothingData();

            ClothingD.Clothes = await _context.Clothing
            .Include(b => b.Brand)
            .Include(b => b.ClothingCategories)
            .ThenInclude(b => b.Category)
            .AsNoTracking()
            .OrderBy(b => b.Name)
            .ToListAsync();
            if (id != null)
            {
                ClothingID = id.Value;
                Clothing clothing = ClothingD.Clothes
                .Where(i => i.ID == id.Value).Single();
                ClothingD.Categories = clothing.ClothingCategories.Select(s => s.Category);
            }

        }
    }
}
