using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;

namespace Roland.Data.Model
{
    public class Engraver : DataModel, IProduct
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        [Range(1, 100)]
        public int MaxSpeed { get; set; }

        [Required]
        [Range(8000, 30000)]
        public int RPM { get; set; }

        [Range(200, 800)]
        public int TableWidth { get; set; }

        [Range(150, 520)]
        public int TableDepth { get; set; }
    }
}
