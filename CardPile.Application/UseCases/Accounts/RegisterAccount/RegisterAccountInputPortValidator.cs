using CardPile.Application.Infrastructure.Validation;
using FluentValidation;

namespace CardPile.Application.UseCases.Accounts.RegisterAccount
{

    public class RegisterAccountInputPortValidator : BaseValidator<RegisterAccountInputPort>
    {

        #region - - - - - - Constructors - - - - - -

        public RegisterAccountInputPortValidator()
        {
            _ = this.RuleFor(i => i.UserName).MaximumLength(30).NotEmpty();
            _ = this.RuleFor(i => i.UserName).MinimumLength(4).NotEmpty();
            _ = this.RuleFor(i => i.Password).MaximumLength(50).NotEmpty();
            _ = this.RuleFor(i => i.Password).MinimumLength(8).NotEmpty();
        }

        #endregion Constructors

    }

}
