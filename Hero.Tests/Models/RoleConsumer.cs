using DotNetStandard.Vent;
using Hero.Services.Events;

namespace Hero.Tests.Models
{
    public class RoleConsumer
    {
        private readonly EventAggregator _vent = EventAggregator.Instance;
        public int Counter { get; set; }
        public dynamic Param { get; set; }
        public RoleConsumer()
        {
            Counter = 0;
            _vent.Subscribe(new RegisterRoleEvent(), Increase);
            _vent.Subscribe(new UnregisterRoleEvent(), Decrease);
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
