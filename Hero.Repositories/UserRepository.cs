using Repositories;
using Repositories.Interfaces;

namespace Hero.Repositories
{
    public interface IUserRepository : IRepository { }

    public class UserRepository : InMemoryRepository, IUserRepository
    {
    }
}
