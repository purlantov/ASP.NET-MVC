using Roland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roland_ASP_MVC.ViewModels.Product
{
    public interface IProductViewModel
    {
        string Model { get; set; }

        ProductType ProductType { get; set; }

    }
}