using System;
using System.Data.Entity;

using Users.DAL.Base;

namespace Users.DAL.EF
{
    public class DbInitializer : CreateDatabaseIfNotExists<UsersContext>
    {
        protected override void Seed(UsersContext context)
        {
            var a1 = new AddressDal { Description = "Work address u1", Value = "WA U1" };
            var a2 = new AddressDal { Description = "Home address u1", Value = "HA U1" };
            var a3 = new AddressDal { Description = "Home address u2", Value = "HA U2" };

            var u1 = new UserDal
                         {
                             Addresses = new[] { a1, a2 },
                             Birthdate = new DateTime(1990, 12, 10),
                             FirstName = "U1FirstName",
                             LastName = "U1LastName",
                             LoginName = "LoginU1",
                             Password = "PasswordU1"
                         };

            var u2 = new UserDal
                         {
                             Addresses = new[] { a3 },
                             Birthdate = new DateTime(2000, 12, 10),
                             FirstName = "U2FirstName",
                             LastName = "U2LastName",
                             LoginName = "LoginU2",
                             Password = "PasswordU2"
                         };

            context.Users.Add(u1);
            context.Users.Add(u2);

            base.Seed(context);
        }
    }
}
