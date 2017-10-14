using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roland.Data.Model;

namespace RolandDG.Services.Contracts
{
    public interface IImpactPrintersService
    {
        IQueryable<ImpactPrinter> GetAll();
        void Add(ImpactPrinter impactPrinter);
        void Delete(ImpactPrinter impactPrinter);
        void Update(ImpactPrinter impactPrinter);
    }
}
