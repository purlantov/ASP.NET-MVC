using Roland.Data.Model.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roland_ASP_MVC.Models
{
    public class Product : DataModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}