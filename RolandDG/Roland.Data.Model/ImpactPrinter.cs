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
    public class ImpactPrinter : DataModel, IProduct
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        public Dictionary<string, int> Resolutions { get; set; }
    }
}
