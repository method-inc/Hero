using System.Linq;
using Hero.Interfaces;
using Hero.Repositories.Interfaces;

namespace Hero.Repositories
{
    public class RoleRepository : InMemoryRepository, IRoleRepository
    {
        public IQueryable<IRole> Get()
        {
            return Get<IRole>();
        }

        public void Create(IRole role)
        {
            Create<IRole>(role);
        }

        public void Delete(IRole role)
        {
            Delete<IRole>(role);
        }
    }
}
