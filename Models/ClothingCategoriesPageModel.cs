using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Proiect.Data;

namespace Proiect.Models
{
    public class ClothingCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(ProiectContext context,
        Clothing clothing)
        {
            var allCategories = context.Category;
            var clothingCategories = new HashSet<int>(
            clothing.ClothingCategories.Select(c => c.ClothingID));
            AssignedCategoryDataList = new List<AssignedCategoryData>();
            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = clothingCategories.Contains(cat.ID)
                });
            }
        }
        public void UpdateBookCategories(ProiectContext context,
        string[] selectedCategories, Clothing clothingToUpdate)
        {
            if (selectedCategories == null)
            {
                clothingToUpdate.ClothingCategories = new List<ClothingCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>
            (clothingToUpdate.ClothingCategories.Select(c => c.Category.ID));
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        clothingToUpdate.ClothingCategories.Add(
                        new ClothingCategory
                        {
                            ClothingID = clothingToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        ClothingCategory courseToRemove
                        = clothingToUpdate
                        .ClothingCategories
                        .SingleOrDefault(i => i.CategoryID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
