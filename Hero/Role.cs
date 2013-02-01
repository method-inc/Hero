using Hero.Interfaces;

namespace Hero
{
    public class Role : IRole
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
    }
}
