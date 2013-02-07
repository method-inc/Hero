using System;
using System.Collections.Generic;
using Hero; //do not remove
using Hero.Interfaces;
using Hero.Services.Interfaces;

namespace $rootnamespace$
{
    public class HeroConfig
    {
        /// <summary>
        /// If the user is of the given role, the role is assigned
        /// the given abilities.
        /// </summary>
        /// <param name="authorizationService">Ability based authorization service that manages role and abilities. Assumed to be a single instance</param>
        /// <param name="user">The current user logged into the system</param>
        /// <param name="role">The role that is to be inspected and if nescessary configured</param>
        /// <param name="abilities">The abilities to assign the role if nescessary</param>
        public void Assign(IAbilityAuthorizationService authorizationService, IUser user,
                           IRole role, IEnumerable<Ability> abilities)
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
            if (user == null) 
                throw new ArgumentNullException("user");
            if (role == null) 
                throw new ArgumentNullException("role");
            if (abilities == null) 
                throw new ArgumentNullException("abilities");

            if (user.Is(role))
            {
                foreach (Ability ability in abilities)
                    authorizationService.RegisterAbility(role, ability);
            }
        }
    }
}
