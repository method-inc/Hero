using Repositories;
using Repositories.Interfaces;

namespace Hero.Repositories
{
    public interface IAbilityRepository : IRepository { }

    public class AbilityRepository : InMemoryRepository, IAbilityRepository
    {
    }
}