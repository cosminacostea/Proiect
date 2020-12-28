using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class Brand
    {
        public int ID { get; set; }
        [Display(Name = "Brand Name")]
        public string BrandName { get; set; }
        public ICollection<Clothing> Clothes { get; set; }
    }
}
