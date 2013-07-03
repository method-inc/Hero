using System.Collections.Generic;
using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IRole : IIdentifiable<string>, INameable
    {
        IList<Ability> Abilities { get; set; }
    }
}