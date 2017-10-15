using Bytes2you.Validation;
using System.Linq;
using Roland.Data.Model;
using Roland.Data.Repositories;
using Roland.Data.UnitOfWork;
using RolandDG.Services.Contracts;

namespace RolandDG.Services
{
    public class UsersService : IUsersService
    {
        private readonly IEfRepository<User> usersRepo;
        private readonly IUnitOfWork unitOfWork;

        public UsersService(IEfRepository<User> usersRepo, IUnitOfWork unitOfWork)
        {
            Guard.WhenArgument(usersRepo, nameof(usersRepo)).IsNull().Throw();
            Guard.WhenArgument(unitOfWork, nameof(unitOfWork)).IsNull().Throw();

            this.usersRepo = usersRepo;
            this.unitOfWork = unitOfWork;
        }

        public IQueryable<User> GetAll()
        {
            return this.usersRepo.All;
        }

        public void Update(User user)
        {
            this.usersRepo.Update(user);
            this.unitOfWork.Commit();
        }

        public void Delete(User user)
        {
            this.usersRepo.Delete(user);
            this.unitOfWork.Commit();
        }
    }
}
