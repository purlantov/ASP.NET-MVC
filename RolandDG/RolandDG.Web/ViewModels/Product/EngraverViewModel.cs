using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Roland.Data.Model;
using RolandDG.Web.Infrastructure;

namespace RolandDG.Web.ViewModels.Product
{
    public class EngraverViewModel : IMapFrom<Engraver>
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public string ProductType { get; set; }

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