using Repositories;
using Repositories.Interfaces;

namespace Hero.Repositories
{
    public interface IRoleRepository : IRepository { }

    public class RoleRepository : InMemoryRepository, IRoleRepository { }
}
