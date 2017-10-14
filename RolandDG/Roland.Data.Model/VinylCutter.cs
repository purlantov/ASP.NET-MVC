using System.ComponentModel.DataAnnotations;
using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;

namespace Roland.Data.Model
{
    public class VinylCutter : DataModel, IProduct
    {
        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        [Required]
        [Range(1, 30)]
        public int CuttingSpeed { get; set; }

        [Required]
        [Range(1, 30)]
        public int BladeForce { get; set; }

        [Required]
        [Range(210, 1620)]
        public int MediaWidth { get; set; }
    }
}
