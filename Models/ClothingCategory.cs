using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect.Models
{
    public class ClothingCategory
    {
        public int ID { get; set; }
        public int ClothingID { get; set; }
        public Clothing Clothing { get; set; }
        public int CategoryID { get; set; }
        public Category Category { get; set; }
    }
}
