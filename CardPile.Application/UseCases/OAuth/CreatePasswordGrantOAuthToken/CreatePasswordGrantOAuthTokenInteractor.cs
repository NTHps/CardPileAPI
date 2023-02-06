using AutoMapper;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken
{

    public class CreatePasswordGrantOAuthTokenInteractor : IUseCaseInteractor<CreatePasswordGrantOAuthTokenInputPort, ICreatePasswordGrantOAuthTokenOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;
        private readonly IJWTFactory m_TokenFactory;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePasswordGrantOAuthTokenInteractor(IMapper mapper, IPersistenceContext persistenceContext, IJWTFactory tokenFactory)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
            this.m_TokenFactory = tokenFactory ?? throw new ArgumentNullException(nameof(tokenFactory));
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
                AccessToken = this.m_TokenFactory.CreateToken(),
                RefreshToken = this.m_TokenFactory.CreateToken(),
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
