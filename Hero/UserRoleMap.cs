using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hero.Interfaces;

namespace Hero
{
    public class UserRoleMap : Dictionary<IUser, HashSet<IRole>> { }
}
