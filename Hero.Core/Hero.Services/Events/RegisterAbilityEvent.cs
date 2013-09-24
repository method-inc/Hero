using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class RegisterAbilityEvent : Event
    {
        public RegisterAbilityEvent() : base("register:ability") { }
    }
}
