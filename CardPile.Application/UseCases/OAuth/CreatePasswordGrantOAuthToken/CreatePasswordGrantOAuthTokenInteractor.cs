using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
using CardPile.Application.Services.Security.Tokens;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken
{

    public class CreatePasswordGrantOAuthTokenInteractor : IUseCaseInteractor<CreatePasswordGrantOAuthTokenInputPort, ICreatePasswordGrantOAuthTokenOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPasswordValidator m_PasswordValidator;
        private readonly IPersistenceContext m_PersistenceContext;
        private readonly ITokenFactory m_TokenFactory;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePasswordGrantOAuthTokenInteractor(IPasswordValidator passwordValidator, IPersistenceContext persistenceContext, ITokenFactory tokenFactory)
        {
            this.m_PasswordValidator = passwordValidator ?? throw CodeContract.ArgumentNullException(nameof(passwordValidator));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
            this.m_TokenFactory = tokenFactory ?? throw CodeContract.ArgumentNullException(nameof(tokenFactory));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(CreatePasswordGrantOAuthTokenInputPort inputPort, ICreatePasswordGrantOAuthTokenOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Account = await this.m_PersistenceContext.GetEntities<Account>()
                 .Where(a => a.UserName == inputPort.Username)
                 .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_grant, "Invalid Username or Password."); ;

            // Validate password
            await this.m_PasswordValidator.ValidatePasswordAsync(_Account, inputPort.Password, CancellationToken.None);

            var _UserToken = this.m_PersistenceContext.GetEntities<UserToken>()
                .SingleOrDefault(ut => ut.Username == inputPort.Username);

            var _ClientApplication = await this.m_PersistenceContext.GetEntities<ClientApplication>()
                .Include(ca => ca.ClientApplicationScope)
                    .ThenInclude(cas => cas.Scope)
                .Where(ca => ca.ClientApplicationID == inputPort.ClientID)
                .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_client, "Invalid Client.");

            IEnumerable<Scope> _Scopes;

            if (!string.IsNullOrEmpty(inputPort.Scope))
                _Scopes = _ClientApplication.ClientApplicationScope.Where(s => s.Scope.Name == inputPort.Scope).Select(cas => cas.Scope);
            else
                _Scopes = _ClientApplication.ClientApplicationScope.Select(cas => cas.Scope);

            if (!_Scopes.Any())
                throw new OAuthException(OAuthErrorValuesEnum.invalid_scope);
            var _ScopeNames = string.Join(',', _Scopes.Select(s => s.Name));

            // TODO: Make some sort of token factory
            var _NewToken = this.m_TokenFactory.GetToken(_ClientApplication, _Scopes);

            if (_UserToken == null)
            {
                _UserToken = new UserToken()
                {
                    Username = inputPort.Username,
                    AccessToken = _NewToken.AccessToken
                };
                this.m_PersistenceContext.Add(_UserToken);
            }
            else
                _UserToken.AccessToken = _NewToken.AccessToken;

            await this.m_PersistenceContext.SaveChangesAsync(CancellationToken.None);

            var _Response = new CreatePasswordGrantOAuthTokenResponse()
            {
                AccessToken = _UserToken.AccessToken,
                RefreshToken = "",
                AccountID = _Account.AccountID,
                GrantType = inputPort.GrantType,
                Scope = _ScopeNames,
                ExpiresIn = DateTime.UtcNow.AddDays(30).ToString()
            };

            await outputPort.PresentAccessToken(_Response, cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
