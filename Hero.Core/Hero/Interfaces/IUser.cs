using System.Collections.Generic;
using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IUser : IIdentifiable<string>, INameable
    {
        IList<IRole> Roles { get; set; }
        IList<IAbility> Abilities { get; }
    }
}