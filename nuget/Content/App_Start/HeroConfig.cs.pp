using System;
using System.Collections.Generic;
using Hero; //do not remove
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace $rootnamespace$
{
    public static class HeroConfig
    {
        public static IAbilityAuthorizationService AuthorizationService { get; private set; }

        public static void Initialize(IAbilityAuthorizationService authorizationService)
        {
            AuthorizationService = authorizationService;
        }

        /// <summary>
        /// Assign a set of abilities to a role.
        /// </summary>
        /// <param name="authorizationService">Ability based authorization service that manages role and abilities. Assumed to be a single instance</param>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="abilities">The abilities to assign the role if nescessary</param>
        public static void AssignAbilitiesToRole(IAbilityAuthorizationService authorizationService, IRole role, IEnumerable<Ability> abilities)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign abilities.
            //
            // You could additionally add the roles and abilities into this file
            // but it was designed to be general enough to support most use cases
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (authorizationService == null)
                throw new ArgumentNullException("authorizationService");
            if (role == null)
                throw new ArgumentNullException("role");
            if (abilities == null)
                throw new ArgumentNullException("abilities");

            foreach (Ability ability in abilities)
                authorizationService.RegisterAbility(role, ability);
        }

        /// <summary>
        /// Assign a set of roles to a user
        /// </summary>
        /// <param name="authorizationService"></param>
        /// <param name="user"></param>
        /// <param name="roles"></param>
        public static void AssignRolesToUser(IAbilityAuthorizationService authorizationService, IUser user, IEnumerable<IRole> roles)
        {
            // This method is intended to be used from the Global.asax.cs or
            // similar. It should only be done from there to encourage a centralized
            // place to assign roles.
            //
            // The method is designed with dependency injection in mind which should allow
            // any method of configuring roles and abilities (i.e. pulling roles from the database,
            // or abilities through refection)

            if (authorizationService == null)
                throw new ArgumentNullException("authorizationService");
            if (user == null)
                throw new ArgumentNullException("user");
            if (roles == null)
                throw new ArgumentNullException("roles");

            foreach (IRole role in roles)
                authorizationService.RegisterRole(user, role);
        }
    }
}
