using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roland.Data.Model;

namespace RolandDG.Services.Contracts
{
    public interface IEngraversService
    {
        IQueryable<Engraver> GetAll();
        void Add(Engraver engraver);
        void Delete(Engraver engraver);
        void Update(Engraver engraver);
    }
}
