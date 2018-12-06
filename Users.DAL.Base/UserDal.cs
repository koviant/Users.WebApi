namespace Users.DAL.Base
{
    using System;
    using System.Collections.Generic;

    public class UserDal
    {
        public int Id { get; set; }

        public DateTime Birthdate { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public virtual ICollection<AddressDal> Addresses { get; set; }
    }
}