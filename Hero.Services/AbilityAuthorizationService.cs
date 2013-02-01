using Hero.Services.Interfaces;

namespace Hero.Services
{
    public abstract class AbilityAuthorizationService : IAuthorizationService, IAbilityAuthorizationService
    {
        public virtual bool Authorize(string abilityName)
        {
            return _Authorize(new Ability(abilityName));
        }

        public virtual bool Authorize(Ability ability)
        {
            return _Authorize(ability);
        }

        private static bool _Authorize(Ability ability)
        {
            return false;
        }
    }
}
