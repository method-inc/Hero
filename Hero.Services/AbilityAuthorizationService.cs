using System.Collections.Generic;
using System.Linq;
using Hero.Interfaces;
using Hero.Repositories;
using Hero.Services.Interfaces;

namespace Hero.Services
{
    public class AbilityAuthorizationService : AuthorizationService, IAbilityAuthorizationService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IAbilityRepository _abilityRepository;

        public AbilityAuthorizationService()
        {
            _userRepository = new UserRepository();
            _roleRepository = new RoleRepository();
            _abilityRepository = new AbilityRepository();
        }

        public AbilityAuthorizationService(IUserRepository userRepository, 
                                           IRoleRepository roleRepository, 
                                           IAbilityRepository abilityRepository)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _abilityRepository = abilityRepository;
        }

        public IEnumerable<IUser> GetUsers()
        {
            return _userRepository.Get<IUser>();
        }

        public IEnumerable<IRole> GetRoles()
        {
            return _roleRepository.Get<IRole>();
        }

        public IEnumerable<IAbility> GetAbilities()
        {
            return _abilityRepository.Get<IAbility>();
        }

        public IUser GetUser(string id)
        {
            return _userRepository.Get<IUser>().FirstOrDefault(user => user.Id == id);
        }

        public IRole GetRole(string id)
        {
            return _roleRepository.Get<IRole>().FirstOrDefault(role => role.Id == id);
        }

        public IAbility GetAbility(string id)
        {
            return _abilityRepository.Get<IAbility>().FirstOrDefault(ability => ability.Id == id);
        }

        public IUser AddUser(IUser user)
        {
            foreach (IRole role in user.Roles)
                AddRole(role);

            _userRepository.Create(user);
            return GetUser(user.Name);
        }

        public IRole AddRole(IRole role)
        {
            foreach (IAbility ability in role.Abilities)
                AddAbility(ability);

            _roleRepository.Create(role);
            return GetRole(role.Name);
        }

        public IAbility AddAbility(IAbility ability)
        {
            _abilityRepository.Create(ability);

            foreach (IAbility childAbility in ability.Abilities)
                AddAbility(childAbility);

            return GetAbility(ability.Name);
        }

        public void RemoveUser(string id)
        {
            RemoveUser((GetUser(id)));
        }

        public void RemoveUser(IUser user)
        {
            _userRepository.Delete(user);
        }

        public void RemoveRole(string id)
        {
            IRole role = GetRole(id);
            RemoveRole(role);
        }

        public void RemoveRole(IRole role)
        {
            foreach (IUser user in _userRepository.Get<IUser>())
                user.Roles.Remove(role);
            _roleRepository.Delete(role);
        }

        public void RemoveAbility(string id)
        {
            IAbility ability = GetAbility(id);
            RemoveAbility(ability);
        }

        public void RemoveAbility(IAbility ability)
        {
            foreach (IRole role in _roleRepository.Get<IRole>())
                role.Abilities.Remove(ability);
            _abilityRepository.Delete(ability);
        }

        public IUser UpdateUser(IUser user)
        {
            RemoveUser(user.Id);
            AddUser(user);
            return GetUser(user.Name);
        }

        public IRole UpdateRole(IRole role)
        {
            RemoveRole(role.Id);
            AddRole(role);
            return GetRole(role.Name);
        }

        public IAbility UpdateAbility(IAbility ability)
        {
            RemoveAbility(ability.Id);
            AddAbility(ability);
            return GetAbility(ability.Name);
        }

        public bool Authorize(IRole role, IAbility ability)
        {
            return _Authorize(role, ability);
        }

        public bool Authorize(string userName, string abilityName)
        {
            IUser user = GetUser(userName);
            IAbility ability = GetAbility(abilityName);
            return Authorize(user, ability);
        }

        public bool Authorize(IUser user, IAbility ability)
        {
            return user.Roles.Any(role => _Authorize(role, ability));
        }

        private bool _Authorize(IRole role, IAbility ability)
        {
            return role.Abilities.Contains(ability);
        }

        public IEnumerable<IAbility> GetAbilitiesForUser(string userName)
        {
            return GetAbilitiesForUser(_userRepository.Get<IUser>().FirstOrDefault(user => user.Name.Equals(userName)));
        }

        public IEnumerable<IAbility> GetAbilitiesForUser(IUser user)
        {
            var abilities = new HashSet<IAbility>();

            foreach (var ability in user.Roles.SelectMany(role => role.Abilities))
                abilities.Add(ability);

            return abilities;
        }
    }
}