using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Roland.Data.Model;
using Roland.Data.Repositories;
using Roland.Data.UnitOfWork;
using RolandDG.Services.Contracts;

namespace RolandDG.Services
{
    public class EngraversService : IEngraversService
    {
        private readonly IEfRepository<Engraver> engraversRepo;
        private readonly IUnitOfWork unitOfWork;

        public EngraversService(IEfRepository<Engraver> productsRepo,IUnitOfWork unitOfWork)
        {
            this.engraversRepo = productsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Engraver> GetAll()
        {
            return this.engraversRepo.All;
        }

        public void Add(Engraver engraver)
        {
            this.engraversRepo.Add(engraver);
            this.unitOfWork.Commit();
        }

        public void Delete(Engraver engraver)
        {
            this.engraversRepo.Delete(engraver);
            this.unitOfWork.Commit();
        }

        public void Update(Engraver engraver)
        {
            this.engraversRepo.Update(engraver);
            this.unitOfWork.Commit();
        }
    }
}
