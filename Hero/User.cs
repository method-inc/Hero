using Hero.Interfaces;

namespace Hero
{
    public class User : IUser
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
