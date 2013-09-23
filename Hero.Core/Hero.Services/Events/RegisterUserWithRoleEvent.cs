using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class RegisterUserWithRoleEvent : Event
    {
        public RegisterUserWithRoleEvent() : base("register:userwithrole") { }
    }
}
