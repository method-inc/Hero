using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class UnregisterUserWithRoleEvent : Event
    {
        public UnregisterUserWithRoleEvent() : base("unregister:userwithrole") { }
    }
}
