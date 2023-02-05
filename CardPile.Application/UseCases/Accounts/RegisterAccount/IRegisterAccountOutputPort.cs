using CardPile.Application.Dtos;
using CardPile.Application.Infrastructure.Validation;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Accounts.RegisterAccount
{

    public interface IRegisterAccountOutputPort :
        IAuthenticationOutputPort,
        IValidationOutputPort<CleanValidationResult>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentAccountRegisteredAsync(AccountDto account, CancellationToken cancellationToken);

        #endregion Methods

    }

}
