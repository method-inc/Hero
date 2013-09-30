using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero.Repositories.Interfaces
{
    public interface IAbilityRepository
    {
        IQueryable<IAbility> Get();
        void Create(IAbility ability);
        void Delete(IAbility ability);
    }
}
