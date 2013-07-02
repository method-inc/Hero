using System.Collections.Generic;
using Hero.Interfaces;
using NGenerics.DataStructures.Trees;

namespace Hero
{
    public class RoleAbilityMap : Dictionary<IRole, List<GeneralTree<Ability>>> { }
}