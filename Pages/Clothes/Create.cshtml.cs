using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Proiect.Data;
using Proiect.Models;

namespace Proiect.Pages.Clothes
{
    public class CreateModel : BookCategoriesPageModel
    {
        private readonly Proiect.Data.ProiectContext _context;

        public CreateModel(Proiect.Data.ProiectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["BrandID"] = new SelectList(_context.Set<Brand>(), "ID", "BrandName");
            var clothing = new Clothing();
            clothing.ClothingCategories = new List<ClothingCategory>();
            PopulateAssignedCategoryData(_context, clothing);
            return Page();
        }

        [BindProperty]
        public Clothing Clothing { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string[] selectedCategories)
        {
            var newClothing = new Clothing();
            if (selectedCategories != null)
            {
                newClothing.ClothingCategories = new List<ClothingCategory>();
                foreach (var cat in selectedCategories)
                {
                    var catToAdd = new ClothingCategory
                    {
                        CategoryID = int.Parse(cat)
                    };
                    newClothing.ClothingCategories.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Clothing>(
            newClothing,
            "Clothing",
            i => i.Name, i => i.Seller,
            i => i.Price, i => i.DateAdded, i => i.BrandID))
            {
                _context.Clothing.Add(newClothing);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedCategoryData(_context, newClothing);
            return Page();

        }
    }
}
