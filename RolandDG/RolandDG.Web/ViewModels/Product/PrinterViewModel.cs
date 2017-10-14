using System;
using System.ComponentModel.DataAnnotations;
using Roland.Data.Model;
using RolandDG.Web.Infrastructure;

namespace RolandDG.Web.ViewModels.Product
{
    public class PrinterViewModel : IMapFrom<Printer>
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType = ProductType.Printer;

        [Required]
        [Range(1, 6)]
        public int PrintHeads { get; set; }

        [Required]
        [Range(500, 1800)]
        public int MediaWidth { get; set; }

        [Required]
        public InkType Ink { get; set; }

        [Required]
        [Range(1, 150)]
        public int MaxSpeed { get; set; }
    }
}