using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Roland.Data.Model;
using Roland_ASP_MVC.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Roland_ASP_MVC.ViewModels.Product
{
    public class PrinterViewModel :IMapFrom<Printer>, IHaveCustomMappings, IProductViewModel
    {
        [Index]
        public Guid Id { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public ProductType ProductType { get; set; }

        public void CreateMappings(IMapperConfigurationExpression configuration)
        {
            configuration.CreateMap<Printer, PrinterViewModel>().
                ForMember(viewModel => viewModel.Model, cfg => cfg.MapFrom(model => model.Model));
        }
    }
}