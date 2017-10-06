using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roland.Data.Model.Contracts
{
    interface IProduct
    {
        string Model { get; set; }

        ProductType ProductType { get; set; }
    }
}
