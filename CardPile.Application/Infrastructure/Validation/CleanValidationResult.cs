using CleanArchitecture.Mediator;
using FluentValidation.Results;

namespace CardPile.Application.Infrastructure.Validation
{

    public class CleanValidationResult : ValidationResult, IValidationResult
    {

        #region - - - - - - Constructors - - - - - -

        public CleanValidationResult(IEnumerable<ValidationFailure> validationFailures) : base(validationFailures) { }

        public CleanValidationResult(ValidationResult validationResult) : base(validationResult.Errors) { }

        #endregion Constructors

    }

}
