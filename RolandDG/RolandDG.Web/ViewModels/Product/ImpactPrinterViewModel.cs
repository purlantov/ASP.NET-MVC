﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Roland.Data.Model;
using RolandDG.Web.Infrastructure;

namespace RolandDG.Web.ViewModels.Product
{
    public class ImpactPrinterViewModel : IMapFrom<ImpactPrinter>
    {
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public string ProductType { get; set; }

        public string Resolution { get; set; }

    }
}