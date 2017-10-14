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
    public class VinylCuttersService : IVinylCuttersService
    {
        private readonly IEfRepository<VinylCutter> vinylCuttersRepo;
        private readonly IUnitOfWork unitOfWork;

        public VinylCuttersService(IEfRepository<VinylCutter> productsRepo, IUnitOfWork unitOfWork)
        {
            this.vinylCuttersRepo = productsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<VinylCutter> GetAll()
        {
            return this.vinylCuttersRepo.All;
        }

        public void Add(VinylCutter vinylCutter)
        {
            this.vinylCuttersRepo.Add(vinylCutter);
            this.unitOfWork.Commit();
        }

        public void Delete(VinylCutter vinylCutter)
        {
            this.vinylCuttersRepo.Delete(vinylCutter);
            this.unitOfWork.Commit();
        }

        public void Update(VinylCutter vinylCutter)
        {
            this.vinylCuttersRepo.Update(vinylCutter);
            this.unitOfWork.Commit();
        }
    }
}
