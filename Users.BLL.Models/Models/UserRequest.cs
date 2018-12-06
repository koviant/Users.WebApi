using System;

using FluentValidation.Attributes;
using Users.BLL.Models.Validators;

namespace Users.BLL.Models
{
    [Validator(typeof(UserRequestValidator))]
    public class UserRequest
    {
        public DateTime Birthdate { get; set; }

        public string LoginName { get; set; }

        public string Password { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}
