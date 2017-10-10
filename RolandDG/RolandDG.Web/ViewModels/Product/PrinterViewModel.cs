using System;
using Roland.Data.Model;
using Roland_ASP_MVC.Infrastructure;

namespace Roland_ASP_MVC.ViewModels.Product
{
    public class PrinterViewModel : IMapFrom<Printer>
    {
        public Guid Id { get; set; }

        public string Model { get; set; }

        public ProductType ProductType { get; set; }
    }
}