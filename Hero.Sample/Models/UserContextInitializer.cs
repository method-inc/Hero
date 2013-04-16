using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebMatrix.WebData;

namespace Hero.Sample.Models
{
    public class UserContextInitializer : CreateDatabaseIfNotExists<UsersContext>
    {
        protected override void Seed(UsersContext usersContext)
        {
            WebSecurity.InitializeDatabaseConnection("DefaultConnection",
               "UserProfile", "UserId", "UserName", autoCreateTables: true);

            var membership = (SimpleMembershipProvider)Membership.Provider;
 
            if (membership.GetUser("todoadminuser", false) == null)
            {
                membership.CreateUserAndAccount("todoadminuser", "password");
            }

            if (membership.GetUser("todobasicuser", false) == null)
            {
                membership.CreateUserAndAccount("todobasicuser", "password");
            }
        }
    }
}