using System.Linq;
using Bytes2you.Validation;
using Roland.Data.Model;
using Roland.Data.Repositories;
using Roland.Data.UnitOfWork;
using RolandDG.Services.Contracts;

namespace RolandDG.Services
{
    public class PrintersService : IPrintersService
    {
        private readonly IEfRepository<Printer> printersRepo;
        private readonly IUnitOfWork unitOfWork;

        public PrintersService(IEfRepository<Printer> printersRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(printersRepo, nameof(printersRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();
            this.printersRepo = printersRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Printer> GetAll()
        {
            return this.printersRepo.All;
        }

        public void Add(Printer printer)
        {
            printersRepo.Add(printer);
            unitOfWork.Commit();
        }

        public void Delete(Printer printer)
        {
            printersRepo.Delete(printer);
            unitOfWork.Commit();
        }

        public void Update(Printer printer)
        {
            printersRepo.Update(printer);
            unitOfWork.Commit();
        }
    }
}
