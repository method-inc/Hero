using System.Collections.Generic;
using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IAbility : IIdentifiable<string>, INameable
    {
        IList<IAbility> Abilities { get; set; }
    }
}