using System.Linq;
using Hero.Interfaces;
using Hero.Repositories.Interfaces;

namespace Hero.Repositories
{
    public class UserRepository : InMemoryRepository, IUserRepository
    {
        public IQueryable<IUser> Get()
        {
            return Get<IUser>();
        }

        public void Create(IUser user)
        {
            Create<IUser>(user);
        }

        public void Delete(IUser user)
        {
            Delete<IUser>(user);
        }
    }
}
