using System.Linq;
using Hero.Interfaces;
using Hero.Repositories.Interfaces;

namespace Hero.Repositories
{
    public class AbilityRepository : InMemoryRepository, IAbilityRepository
    {
        public IQueryable<IAbility> Get()
        {
            return Get<IAbility>();
        }

        public void Create(IAbility ability)
        {
            Create<IAbility>(ability);
        }

        public void Delete(IAbility ability)
        {
            Delete<IAbility>(ability);
        }
    }
}