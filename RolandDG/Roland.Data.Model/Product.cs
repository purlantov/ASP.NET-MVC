using Roland.Data.Model.Abstracts;
using Roland.Data.Model.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Roland.Data.Model
{
    public class Product : DataModel
    {
        public string Category { get; set; }

        public decimal Price { get; set; }
    }
}