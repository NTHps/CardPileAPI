using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
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

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePasswordGrantOAuthTokenInteractor(IPasswordValidator passwordValidator, IPersistenceContext persistenceContext)
        {
            this.m_PasswordValidator = passwordValidator ?? throw CodeContract.ArgumentNullException(nameof(passwordValidator));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(CreatePasswordGrantOAuthTokenInputPort inputPort, ICreatePasswordGrantOAuthTokenOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Account = await this.m_PersistenceContext.GetEntities<Account>()
                 .Where(a => a.UserName == inputPort.Username)
                 .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_grant, "Invalid Username or Password."); ;
            //if (_Account == null)
            //    return outputPort.PresentUnauthenticatedAsync(cancellationToken);

            // Validate password
            await this.m_PasswordValidator.ValidatePasswordAsync(_Account, inputPort.Password, CancellationToken.None);

            var _UserToken = this.m_PersistenceContext.GetEntities<UserToken>()
                .SingleOrDefault(ut => ut.Username == inputPort.Username);

            var _ClientApplication = await this.m_PersistenceContext.GetEntities<ClientApplication>()
                .Where(ca => ca.ClientApplicationID == inputPort.ClientID)
                .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_client, "Invalid Client.");

            // TODO: Make some sort of token factory
            var _NewToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Guid.NewGuid()}:{Guid.NewGuid()}"));

            if (_UserToken == null)
            {
                _UserToken = new UserToken()
                {
                    Username = inputPort.Username,
                    Token = _NewToken
                };
                this.m_PersistenceContext.Add(_UserToken);
            }
            else
                _UserToken.Token = _NewToken;

            await this.m_PersistenceContext.SaveChangesAsync(CancellationToken.None);

            var _Response = new CreatePasswordGrantOAuthTokenResponse()
            {
                AccessToken = _NewToken,
                RefreshToken = "",
                AccountID = _Account.AccountID,
                GrantType = "",
                Scope = "",
                ExpiresIn = DateTime.UtcNow.AddDays(30).ToString()
            };

            await outputPort.PresentAccessToken(_Response, cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
