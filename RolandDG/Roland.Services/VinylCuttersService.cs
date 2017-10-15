using System.Linq;
using Bytes2you.Validation;
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

        public VinylCuttersService(IEfRepository<VinylCutter> vinylCutterRepoRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(vinylCutterRepoRepo, nameof(vinylCutterRepoRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.vinylCuttersRepo = vinylCutterRepoRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<VinylCutter> GetAll()
        {
            return vinylCuttersRepo.All;
        }

        public void Add(VinylCutter vinylCutter)
        {
            vinylCuttersRepo.Add(vinylCutter);
            unitOfWork.Commit();
        }

        public void Delete(VinylCutter vinylCutter)
        {
            vinylCuttersRepo.Delete(vinylCutter);
            unitOfWork.Commit();
        }

        public void Update(VinylCutter vinylCutter)
        {
            vinylCuttersRepo.Update(vinylCutter);
            unitOfWork.Commit();
        }
    }
}
