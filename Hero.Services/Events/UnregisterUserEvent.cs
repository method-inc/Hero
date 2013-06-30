using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    class UnregisterUserEvent : Event
    {
        public UnregisterUserEvent() : base("unregister:user") { }
    }
}
