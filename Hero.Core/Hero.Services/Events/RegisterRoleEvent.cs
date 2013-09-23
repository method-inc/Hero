using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    class RegisterRoleEvent : Event
    {
        public RegisterRoleEvent() : base("register:role") { }
    }
}
