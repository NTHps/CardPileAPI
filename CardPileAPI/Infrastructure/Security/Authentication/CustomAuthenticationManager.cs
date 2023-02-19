using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
using CardPile.Domain.Entities;
using CardPileAPI.Services.Security.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace CardPileAPI.Infrastructure.Security.Authentication
{

    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPasswordValidator m_PasswordValidator;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomAuthenticationManager(IPasswordValidator passwordValidator, IPersistenceContext persistenceContext)
        {
            this.m_PasswordValidator = passwordValidator ?? throw CodeContract.ArgumentNullException(nameof(passwordValidator));
            this.m_PersistenceContext = persistenceContext ?? throw CodeContract.ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - CustomAuthenticationManager Implementation - - - - - -

        public async Task<string> AuthenticateAsync(long clientID, string username, string password)
        {
            // Check if account exists
            var _Account = await this.m_PersistenceContext.GetEntities<Account>()
                .Where(a => a.UserName == username)
                .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_grant, "Invalid Username or Password."); ;

            // Validate password
            await this.m_PasswordValidator.ValidatePasswordAsync(_Account, password, CancellationToken.None);

            var _UserToken = await this.m_PersistenceContext.GetEntities<UserToken>()
                .SingleOrDefaultAsync(ut => ut.Username == username);

            var _ClientApplication = await this.m_PersistenceContext.GetEntities<ClientApplication>()
                .Where(ca => ca.ClientApplicationID == clientID)
                .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_client, "Invalid Client.");

            // TODO: Make some sort of token factory
            var _NewToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{Guid.NewGuid()}:{Guid.NewGuid()}"));

            if (_UserToken == null)
            {
                _UserToken = new UserToken()
                {
                    Username = username,
                    Token = _NewToken
                };
                this.m_PersistenceContext.Add(_UserToken);
            }
            else
                _UserToken.Token = _NewToken;

            await this.m_PersistenceContext.SaveChangesAsync(CancellationToken.None);

            return _UserToken.Token;
        }

        #endregion CustomAuthenticationManager Implementation

    }

}
