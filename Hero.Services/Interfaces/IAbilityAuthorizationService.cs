using Hero.Interfaces;

namespace Hero.Services.Interfaces
{
    public interface IAbilityAuthorizationService
    {
        bool Authorize(IRole role, Ability ability);
        void RegisterAbility(IRole role, Ability ability);
        void UnregisterAbility(IRole role, Ability ability);
    }
}