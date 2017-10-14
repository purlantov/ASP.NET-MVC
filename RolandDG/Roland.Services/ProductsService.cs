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

        public void Add(Product product)
        {
            productsRepo.Add(product);
            unitOfWork.Commit();
        }

        public void Delete(Product product)
        {
            this.productsRepo.Delete(product);
            this.unitOfWork.Commit();
        }

        public void Update(Product product)
        {
            this.productsRepo.Update(product);
            this.unitOfWork.Commit();
        }
    }
}
