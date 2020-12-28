using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Clothes
{
    public class DetailsModel : ClothingCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public DetailsModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public Clothing Clothing { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Clothing = await _context.Clothing
                .Include(b => b.Brand)
                .Include(b => b.ClothingCategories).ThenInclude(b => b.Category)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);


            if (Clothing == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(_context, Clothing);
            ViewData["BrandID"] = new SelectList(_context.Set<Brand>(), "ID", "BrandName");
            return Page();
        }
    }
}
