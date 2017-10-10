using System.Linq;
using Roland.Data.Model;
using Roland.Data.Repositories;
using Roland.Data.UnitOfWork;

namespace RolandDG.Services.Contracts
{
    public class PrintersService : IPrintersService
    {
        private readonly IEfRepository<Printer> printersRepo;
        private readonly IUnitOfWork unitOfWork;

        public PrintersService(IEfRepository<Printer> productsRepo, IUnitOfWork unitOfWork)
        {
            this.printersRepo = productsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Printer> GetAll()
        {
            return this.printersRepo.All;
        }

        public void Add(Printer printer)
        {
            this.printersRepo.Add(printer);
            this.unitOfWork.Commit();
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
