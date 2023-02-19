using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken
{

    public class CreatePasswordGrantOAuthTokenInteractor : IUseCaseInteractor<CreatePasswordGrantOAuthTokenInputPort, ICreatePasswordGrantOAuthTokenOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePasswordGrantOAuthTokenInteractor(IPersistenceContext persistenceContext)
        {
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(CreatePasswordGrantOAuthTokenInputPort inputPort, ICreatePasswordGrantOAuthTokenOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Account = this.m_PersistenceContext.GetEntities<Account>()
                .SingleOrDefault(a => a.UserName == inputPort.Username);
            if (_Account == null)
                return outputPort.PresentUnauthenticatedAsync(cancellationToken);

            var _Response = new CreatePasswordGrantOAuthTokenResponse()
            {
                AccessToken = "",
                RefreshToken = "",
                AccountID = _Account.AccountID,
                GrantType = "",
                Scope = "",
                ExpiresIn = DateTime.UtcNow.AddDays(30).ToString()
            };

            return outputPort.PresentAccessToken(_Response, cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
