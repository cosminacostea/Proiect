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
    public class EditModel : ClothingCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public EditModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
        {
            if (id == null)
            {
                return NotFound();
            }
            var clothingToUpdate = await _context.Clothing
            .Include(i => i.Brand)
            .Include(i => i.ClothingCategories)
            .ThenInclude(i => i.Category)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (clothingToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Clothing>(
            clothingToUpdate,
            "Book",
            i => i.Name, i => i.Seller,
            i => i.Price, i => i.DateAdded, i => i.Brand))
            {
                UpdateBookCategories(_context, selectedCategories, clothingToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            UpdateBookCategories(_context, selectedCategories, clothingToUpdate);
            PopulateAssignedCategoryData(_context, clothingToUpdate);
            return Page();
        }


        private bool ClothingExists(int id)
        {
            return _context.Clothing.Any(e => e.ID == id);
        }
    }
}
