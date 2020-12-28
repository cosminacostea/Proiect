using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect.Models
{
    public class ClothingData
    {
        public IEnumerable<Clothing> Clothes { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<ClothingCategory> ClothingCategories { get; set; }

    }
}
