using FluentValidation.Attributes;
using Users.BLL.Models.Validators;

namespace Users.BLL.Models
{
    [Validator(typeof(AddressRequestValidator))]
    public class AddressRequest
    {
        public string Description { get; set; }

        public string Value { get; set; }
    }
}
