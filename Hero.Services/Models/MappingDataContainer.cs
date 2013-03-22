using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero.Services.Models
{
    public class MappingDataContainer
    {
        public MappingDataContainer(RoleAbilityMap roleAbilityMap, UserRoleMap userRoleMap)
        {
            Abilities = new List<Ability>();
            Roles = new List<IRole>();
            Users = new List<IUser>();
            UserToRoles = new List<Tuple<IUser, IRole>>();
            RoleToAbilities = new List<Tuple<IRole, Ability>>();

            foreach (var roleAbility in roleAbilityMap)
            {
                var role = roleAbility.Key;
                Roles.Add(role);

                foreach (var ability in roleAbility.Value)
                {
                    if (!Abilities.Contains(ability))
                    {
                        Abilities.Add(ability);
                    }
                    RoleToAbilities.Add(new Tuple<IRole, Ability>(role, ability));
                }
            }

            foreach (var userRole in userRoleMap)
            {
                var user = userRole.Key;
                Users.Add(user);

                foreach (var role in userRole.Value)
                {
                    if (!Roles.Contains(role))
                    {
                        Roles.Add(role);
                    }
                    UserToRoles.Add(new Tuple<IUser, IRole>(user, role));
                }
            }
        }

        public List<Ability> Abilities { get; set; }
        public List<IRole> Roles { get; set; }
        public List<IUser> Users { get; set; }
        public List<Tuple<IRole, Ability>> RoleToAbilities { get; set; }
        public List<Tuple<IUser, IRole>> UserToRoles { get; set; }
    }
}
