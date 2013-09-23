using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    public class UnregisterAbilityEvent : Event
    {
        public UnregisterAbilityEvent() : base("unregister:ability") { }
    }
}
