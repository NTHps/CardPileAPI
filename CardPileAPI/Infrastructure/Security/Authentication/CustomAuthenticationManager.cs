using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
using CardPile.Domain.Entities;
using CardPileAPI.Services.Security.Authentication;
using Microsoft.EntityFrameworkCore;

namespace CardPileAPI.Infrastructure.Security.Authentication
{

    public class CustomAuthenticationManager : ICustomAuthenticationManager
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPasswordValidator m_PasswordValidator;
        private readonly IPersistenceContext m_PersistenceContext;

        private readonly IDictionary<string, string> m_Tokens = new Dictionary<string, string>();

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomAuthenticationManager(IPasswordValidator passwordValidator, IPersistenceContext persistenceContext)
        {
            this.m_PasswordValidator = passwordValidator ?? throw CodeContract.ArgumentNullException(nameof(passwordValidator));
            this.m_PersistenceContext = persistenceContext ?? throw CodeContract.ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IDictionary<string, string> Tokens => m_Tokens;

        #endregion Properties

        #region - - - - - - CustomAuthenticationManager Implementation - - - - - -

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Check if account exists
            var _Account = await this.m_PersistenceContext.GetEntities<Account>()
                .Where(a => a.UserName == username)
                .SingleOrDefaultAsync()
                    ?? throw new OAuthException(OAuthErrorValuesEnum.invalid_grant, "Invalid Username or Password."); ;

            // Validate password
            await this.m_PasswordValidator.ValidatePasswordAsync(_Account, password, CancellationToken.None);

            // Generate token, save it 
            var _Token = Guid.NewGuid().ToString();
            this.m_Tokens.Add(_Token, username);

            return _Token;
        }

        #endregion CustomAuthenticationManager Implementation

    }

}
