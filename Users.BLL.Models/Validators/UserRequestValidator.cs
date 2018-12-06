using FluentValidation;
using Constants = ConfigConstants.ConfigConstants;

namespace Users.BLL.Models.Validators
{ 
    class UserRequestValidator : AbstractValidator<UserRequest>
    {
        public UserRequestValidator()
        {
            RuleFor(u => u.FirstName).NotNull().NotEmpty().MaximumLength(Constants.UserFirstNameLength);
            RuleFor(u => u.LastName).NotNull().NotEmpty().MaximumLength(Constants.UserLastNameLength);
            RuleFor(u => u.LoginName).NotNull().NotEmpty().MaximumLength(Constants.UserLoginNameLength);
            RuleFor(u => u.Password).NotNull().NotEmpty().MaximumLength(Constants.UserPasswordLength);
        }
    }
}
