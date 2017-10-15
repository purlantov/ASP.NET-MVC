using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roland.Data.Model.Contracts
{
    public interface IProduct
    {
        string Model { get; set; }

        string ProductType { get; set; }
    }
}
