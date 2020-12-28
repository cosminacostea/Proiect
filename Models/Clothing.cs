using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Clothing
    {
        public int ID { get; set; }
        [Required, StringLength(150, MinimumLength = 3)]
        [Display(Name = "Item Name")]
        public string Name { get; set; }
        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage = "The name of the seller must be of type 'FirstName LastName"), Required, StringLength(50, MinimumLength = 3)]
        public string Seller { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        [Range(1, 1000)]
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Date Added")]
        public DateTime DateAdded { get; set; }
        public int BrandID { get; set; }
        public Brand Brand { get; set; }
        [Display(Name = "Clothing Category")]
        public ICollection<ClothingCategory> ClothingCategories { get; set; }
    }
}
