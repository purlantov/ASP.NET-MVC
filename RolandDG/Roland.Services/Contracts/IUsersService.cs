using Roland.Data.Model;
using System.Linq;

namespace RolandDG.Services.Contracts
{
    public interface IUsersService
    {
        IQueryable<User> GetAll();
        void Delete(User user);
        void Update(User user);
    }
}
