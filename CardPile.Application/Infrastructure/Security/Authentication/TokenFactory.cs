using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Services.Security.Tokens;
using CardPile.Domain.Entities;
using System.Text;

namespace CardPile.Application.Infrastructure.Security.Authentication
{

    public class TokenFactory : ITokenFactory
    {

        #region - - - - - - Fields - - - - - -


        #endregion Fields

        #region - - - - - - Constructors - - - - - -


        #endregion

        #region - - - - - - ITokenFactory Implementation - - - - - -

        public UserToken GetToken(ClientApplication clientApplication, IEnumerable<Scope> scopes)
        {
            if (clientApplication == null)
                throw CodeContract.ArgumentNullException(nameof(clientApplication));
            if (scopes == null)
                throw CodeContract.ArgumentNullException(nameof(scopes));

            var _AccessToken = this.CreateAccessToken(clientApplication, scopes);

            return new UserToken()
            {
                AccessToken = _AccessToken,
                TokenType = UserToken.DefaultAuthenticationScheme
            };
        }

        #endregion ITokenFactory Implementation

        #region - - - - - - Methods - - - - - -

        private string CreateAccessToken(ClientApplication clientApplication, IEnumerable<Scope> scopes)
        {
            var _Claims = new AccessTokenClaims();
            var _SecretClaims = new AccessTokenSecretClaims
            {
                ClientName = clientApplication.Name,
                ScopeNames = this.GetScopeNamesString(scopes),
            };
            //return this.m_AccessTokenEncoder.Encode(new TokenComponents<AccessTokenClaims, AccessTokenSecretClaims>(_Claims, _SecretClaims));

            // TO DO: Access Token Encoder
            var _NewToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{_SecretClaims.ScopeNames}:{Guid.NewGuid()}"));
            return _NewToken;
        }

        private string GetScopeNamesString(IEnumerable<Scope> scopes) => string.Join(",", scopes.Select(s => s.Name));

        #endregion Methods

    }

}
