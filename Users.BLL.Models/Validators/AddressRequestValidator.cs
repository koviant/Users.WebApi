using FluentValidation;
using Constants = ConfigConstants.ConfigConstants;

namespace Users.BLL.Models.Validators
{
    public class AddressRequestValidator : AbstractValidator<AddressRequest>
    {
        public AddressRequestValidator()
        {
            RuleFor(ar => ar.Description).NotNull().NotEmpty().MaximumLength(Constants.AddressDescriptionLength);
            RuleFor(ar => ar.Value).NotNull().NotEmpty().MaximumLength(Constants.AddressValueLength);
        }
    }
}