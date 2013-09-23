using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DotNetStandard.Vent;

namespace Hero.Services.Events
{
    class RegisterUserEvent : Event
    {
        public RegisterUserEvent() : base("register:user") { }
    }
}
