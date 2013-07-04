using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    class UnregisterRoleEvent : Event
    {
        public UnregisterRoleEvent() : base("unregister:role") { }
    }
}
