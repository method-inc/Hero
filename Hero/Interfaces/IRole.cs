using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IRole : IIdentifiable<string>, INameable { }
}