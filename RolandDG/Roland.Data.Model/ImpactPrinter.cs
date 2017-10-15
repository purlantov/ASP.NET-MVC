using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;

namespace Roland.Data.Model
{
    public class ImpactPrinter : DataModel, IProduct
    {
        public ImpactPrinter()
        {
            ProductType = "ImpactPrinter";
        }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public string ProductType { get; set; }

        //public Dictionary<string, int> Resolutions { get; set; }

        public int Resolution { get; set; }
    }
}
