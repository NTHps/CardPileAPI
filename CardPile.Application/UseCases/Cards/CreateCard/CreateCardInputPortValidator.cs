using CardPile.Application.Infrastructure.Validation;
using FluentValidation;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardInputPortValidator : BaseValidator<CreateCardInputPort>
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCardInputPortValidator()
            => _ = this.RuleFor(i => i.Name).MaximumLength(100).NotEmpty();

        #endregion Constructors

    }

}
