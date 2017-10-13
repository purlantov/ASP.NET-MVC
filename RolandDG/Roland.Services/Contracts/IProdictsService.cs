using Roland.Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RolandDG.Services.Contracts
{
    public interface IProdictsService
    {
        IQueryable<Product> GetAll();
        void Add(Product product);
        void Delete(Product laptop);
        void Update(Product laptop);
    }
}
