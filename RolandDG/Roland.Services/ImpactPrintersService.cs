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
    public class ImpactPrintersService : IImpactPrintersService
    {
        private readonly IEfRepository<ImpactPrinter> printersRepo;
        private readonly IUnitOfWork unitOfWork;

        public ImpactPrintersService(IEfRepository<ImpactPrinter> productsRepo, IUnitOfWork unitOfWork)
        {
            this.printersRepo = productsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<ImpactPrinter> GetAll()
        {
            return this.printersRepo.All;

        }

        public void Add(ImpactPrinter impactPrinter)
        {
            this.printersRepo.Add(impactPrinter);
            this.unitOfWork.Commit();
        }

        public void Delete(ImpactPrinter impactPrinter)
        {
            this.printersRepo.Delete(impactPrinter);
            this.unitOfWork.Commit();
        }

        public void Update(ImpactPrinter impactPrinter)
        {
            this.printersRepo.Update(impactPrinter);
            this.unitOfWork.Commit();
        }
    }
}
