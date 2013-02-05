using DotNetStandard.Interfaces;

namespace Hero.Interfaces
{
    public interface IUser : IIdentifiable<int>, INameable
    {
        bool Is(IRole role);
    }
}