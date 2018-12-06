using System;
using System.Collections.Generic;

namespace Users.BLL.Models
{
    public class User
    {
        public int Id { get; set; }

        public DateTime Birthdate { get; set; }

        public List<Address> Addresses { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public User()
        {
            Addresses = new List<Address>();
        }
    }
}
