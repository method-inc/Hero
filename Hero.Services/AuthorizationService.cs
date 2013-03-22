using System;
using Hero.Services.Interfaces;

namespace Hero.Services
{
    public abstract class AuthorizationService : IAuthorizationService
    {
        public virtual bool Authorize(string entity)
        {
            throw new NotImplementedException();
        }

        public virtual string SerializeAbilities()
        {
            throw new NotImplementedException();
        }
    }
}
