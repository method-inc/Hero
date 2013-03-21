using DotNetStandard.Vent;
using Hero.Services.Events;

namespace Hero.Tests.Models
{
    public class AbilityConsumer
    {
        private readonly EventAggregator _vent = EventAggregator.Instance;
        public int Counter { get; set; }
        public dynamic Param { get; set; }
        public AbilityConsumer()
        {
            Counter = 0;
            _vent.Subscribe(new RegisterAbilityEvent(), Increase);
            _vent.Subscribe(new UnregisterAbilityEvent(), Decrease);
        }

        public void Increase(dynamic param)
        {
            Counter++;
            Param = param;
        }

        public void Decrease(dynamic param)
        {
            Counter--;
            Param = param;
        }
    }
}
