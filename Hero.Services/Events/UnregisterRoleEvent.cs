using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class UnregisterRoleEvent : Event
    {
        public UnregisterRoleEvent() : base("unregister:role") { }
    }
}
