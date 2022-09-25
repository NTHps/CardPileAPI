using CleanArchitecture.Mediator;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

namespace CardPile.Application.Infrastructure.Validation
{

    public abstract class BaseValidator<TInputPort> :
        AbstractValidator<TInputPort>,
        IUseCaseInputPortValidator<TInputPort, CleanValidationResult>
        where TInputPort : IUseCaseInputPort<IValidationOutputPort<CleanValidationResult>>
    {

        #region - - - - - - Methods - - - - - -

        Task<CleanValidationResult> IUseCaseInputPortValidator<TInputPort, CleanValidationResult>.ValidateAsync(TInputPort inputPort, CancellationToken cancellationToken)
            => Task.FromResult(new CleanValidationResult(this.Validate(inputPort)));

        #endregion Methods

    }

}
