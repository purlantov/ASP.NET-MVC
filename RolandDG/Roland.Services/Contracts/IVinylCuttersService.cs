using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roland.Data.Model;

namespace RolandDG.Services.Contracts
{
    public interface IVinylCuttersService
    {
        IQueryable<VinylCutter> GetAll();
        void Add(VinylCutter vinylCutter);
        void Delete(VinylCutter vinylCutter);
        void Update(VinylCutter vinylCutter);
    }
}
