using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Roland.Data.Model
{
    public class Printer : DataModel, IProduct
    {
        public Printer()
        {
            ProductType = "Printer";
        }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public string ProductType { get; set; }

        [Required]
        [Range(1,6)]
        public int PrintHeads { get; set; }

        [Required]
        [Range(500,1800)]
        public int MediaWidth { get; set; }

        [Required]
        public InkType Ink { get; set; }

        [Required]
        [Range(1, 150)]
        public int MaxSpeed { get; set; }
    }
}
