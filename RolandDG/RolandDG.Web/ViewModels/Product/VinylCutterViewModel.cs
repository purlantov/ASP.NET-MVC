using System;
using System.ComponentModel.DataAnnotations;
using Roland.Data.Model;
using RolandDG.Web.Infrastructure;

namespace RolandDG.Web.ViewModels.Product
{
    public class VinylCutterViewModel : IMapFrom<VinylCutter>
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType = ProductType.VinylCutter;

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