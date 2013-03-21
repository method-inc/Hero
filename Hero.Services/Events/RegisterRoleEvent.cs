using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class RegisterRoleEvent : Event
    {
        public RegisterRoleEvent() : base("register:role") { }
    }
}
