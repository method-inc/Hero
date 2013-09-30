using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero.Repositories.Interfaces
{
    public interface IUserRepository
    {
        IQueryable<IUser> Get();
        void Create(IUser user);
        void Delete(IUser user);
    }
}
