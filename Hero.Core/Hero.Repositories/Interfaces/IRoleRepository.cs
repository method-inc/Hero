using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        IQueryable<IRole> Get();
        void Create(IRole role);
        void Delete(IRole role);
    }
}
