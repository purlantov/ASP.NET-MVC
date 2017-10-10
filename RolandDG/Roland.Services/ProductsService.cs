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
    public class ProductsService : IProdictsService
    {
        private readonly IEfRepository<Product> productsRepo;
        private readonly IUnitOfWork unitOfWork;

        public ProductsService(IEfRepository<Product> productsRepo, IUnitOfWork unitOfWork )
        {
            this.productsRepo = productsRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<Product> GetAll()
        {
            return productsRepo.All;
        }

        public void Add(Product printer)
        {
            productsRepo.Add(printer);
            unitOfWork.Commit();
        }

        public void Delete(Product laptop)
        {
            throw new NotImplementedException();
        }

        public void Update(Product laptop)
        {
            throw new NotImplementedException();
        }
    }
}
