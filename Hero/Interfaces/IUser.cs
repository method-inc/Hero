using System.Collections.Generic;
using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IUser : IIdentifiable<string>, INameable
    {
        IList<Role> Roles { get; set; }
        IList<Ability> Abilities { get; }
    }
}