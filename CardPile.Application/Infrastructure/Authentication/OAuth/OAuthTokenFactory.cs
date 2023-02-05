using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Services.OAuth;
using CardPile.Application.Services.OAuth.Tokens;
using CardPile.Domain.Entities;
using Microsoft.Extensions.Options;
using System.Text;

namespace CardPile.Application.Infrastructure.Authentication.OAuth
{

    public class OAuthTokenFactory : IOAuthTokenFactory
    {

        #region - - - - - - Fields - - - - - -

        private readonly ITokenEncoder<AccessTokenClaims, AccessTokenSecretClaims> m_AccessTokenEncoder;
        private readonly OAuthOptions m_OAuthOptions;
        private readonly ITokenEncoder<RefreshTokenClaims, RefreshTokenSecretClaims> m_RefreshTokenEncoder;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OAuthTokenFactory
        (
            IOptions<OAuthOptions> oAuthOptions,
            ITokenEncoder<AccessTokenClaims, AccessTokenSecretClaims> accessTokenEncoder,
            ITokenEncoder<RefreshTokenClaims, RefreshTokenSecretClaims> refreshTokenEncoder
        )
        {
            this.m_OAuthOptions = oAuthOptions?.Value ?? throw CodeContract.ArgumentNullException(nameof(oAuthOptions));
            this.m_AccessTokenEncoder = accessTokenEncoder ?? throw CodeContract.ArgumentNullException(nameof(accessTokenEncoder));
            this.m_RefreshTokenEncoder = refreshTokenEncoder ?? throw CodeContract.ArgumentNullException(nameof(refreshTokenEncoder));
        }

        #endregion

        #region - - - - - - IOAuthTokenFactory Implementation - - - - - -

        public OAuthToken GetToken(ClientApplication clientApplication, IEnumerable<Scope> scopes)
        {
            if (clientApplication == null)
                throw CodeContract.ArgumentNullException(nameof(clientApplication));
            if (scopes == null)
                throw CodeContract.ArgumentNullException(nameof(scopes));

            var _AccessToken = this.CreateAccessToken(clientApplication, scopes);

            return new OAuthToken()
            {
                AccessToken = _AccessToken,
                TokenType = OAuthToken.DefaultAuthenticationScheme
            };
        }

        public OAuthToken GetToken(Account account, ClientApplication clientApplication, IEnumerable<Scope> scopes, ICollection<MetaData> metaData)
        {
            if (account == null)
                throw CodeContract.ArgumentNullException(nameof(account));
            if (clientApplication == null)
                throw CodeContract.ArgumentNullException(nameof(clientApplication));
            if (scopes == null)
                throw CodeContract.ArgumentNullException(nameof(scopes));

            var _AccessToken = this.CreateAccessToken(account, clientApplication, scopes, metaData);
            var _RefreshToken = this.CreateRefreshToken(account, clientApplication, scopes, metaData);

            return new OAuthToken()
            {
                AccessToken = _AccessToken,
                TokenType = OAuthToken.DefaultAuthenticationScheme,
                ExpiresIn = this.m_OAuthOptions.AccessTokenTimeToLiveSeconds,
                RefreshToken = _RefreshToken
            };
        }

        public string GetToken()
        {
            var _Token = new StringBuilder();
            for (var _Index = 0; _Index < 10; _Index++)
                _ = _Token.Append(Guid.NewGuid().ToString().Replace("-", string.Empty));
            return _Token.ToString();
        }

        #endregion

        #region - - - - - - Methods - - - - - -

        private string CreateAccessToken(ClientApplication clientApplication, IEnumerable<Scope> scopes)
        {
            var _Claims = new AccessTokenClaims();
            var _SecretClaims = new AccessTokenSecretClaims
            {
                ClientName = clientApplication.Name,
                ScopeNames = this.GetScopeNamesString(scopes),
            };

            return this.m_AccessTokenEncoder.Encode(new TokenComponents<AccessTokenClaims, AccessTokenSecretClaims>(_Claims, _SecretClaims));
        }

        private string CreateAccessToken(Account account, ClientApplication clientApplication, IEnumerable<Scope> scopes, ICollection<MetaData> metaData)
        {
            var _Claims = new AccessTokenClaims()
            {
                AccountID = account.AccountID,
                ClientID = clientApplication.ClientApplicationID,
                ScopeIDs = this.GetScopeIDsString(scopes),
                MetaData = metaData
            };
            var _SecretClaims = new AccessTokenSecretClaims
            {
                AccountEmail = account.Email,
                ClientName = clientApplication.Name,
                ScopeNames = this.GetScopeNamesString(scopes)
            };

            return this.m_AccessTokenEncoder.Encode(new TokenComponents<AccessTokenClaims, AccessTokenSecretClaims>(_Claims, _SecretClaims));
        }

        private string CreateRefreshToken(Account account, ClientApplication clientApplication, IEnumerable<Scope> scopes, ICollection<MetaData> metaData)
        {
            var _Claims = new RefreshTokenClaims()
            {
                AccountID = account.AccountID,
                ClientID = clientApplication.ClientApplicationID,
                MetaData = metaData,
                ScopeIDs = this.GetScopeIDsString(scopes)
            };
            var _SecretClaims = new RefreshTokenSecretClaims
            {
                AccountEmail = account.Email,
                ClientName = clientApplication.Name,
                ScopeNames = this.GetScopeNamesString(scopes)
            };

            return this.m_RefreshTokenEncoder.Encode(new TokenComponents<RefreshTokenClaims, RefreshTokenSecretClaims>(_Claims, _SecretClaims));
        }

        private string GetScopeIDsString(IEnumerable<Scope> scopes) => string.Join(",", scopes.Select(s => s.ScopeID));

        private string GetScopeNamesString(IEnumerable<Scope> scopes) => string.Join(",", scopes.Select(s => s.Name));

        #endregion Methods

    }

}
