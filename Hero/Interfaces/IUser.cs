using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IUser : IIdentifiable<int>, INameable { }
}