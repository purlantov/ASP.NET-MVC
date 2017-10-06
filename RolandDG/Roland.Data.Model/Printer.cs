using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Roland.Data.Model
{
    public class Printer : DataModel, IProduct
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType { get; set; }
    }
}
