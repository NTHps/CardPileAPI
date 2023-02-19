using CardPile.Application.Infrastructure.Validation;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken
{

    public interface ICreatePasswordGrantOAuthTokenOutputPort :
        IAuthenticationOutputPort,
        IValidationOutputPort<CleanValidationResult>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentAccessToken(CreatePasswordGrantOAuthTokenResponse response, CancellationToken cancellationToken);

        #endregion Methods

    }

}
