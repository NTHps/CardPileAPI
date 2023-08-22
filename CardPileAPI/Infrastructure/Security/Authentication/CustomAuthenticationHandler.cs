using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace CardPileAPI.Infrastructure.Security.Authentication
{

    public class CustomAuthenticationHandler : AuthenticationHandler<BasicAuthenticationOptions>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructor - - - - - -

        public CustomAuthenticationHandler(IOptionsMonitor<BasicAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            IPersistenceContext persistenceContext)
            : base(options, logger, encoder, clock)
        {
            this.m_PersistenceContext = persistenceContext;
        }

        #endregion Constructor

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Unauthorized");

            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
            {
                return AuthenticateResult.NoResult();
            }

            if (!authorizationHeader.StartsWith("bearer", StringComparison.OrdinalIgnoreCase))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            string token = authorizationHeader.Substring("bearer".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return AuthenticateResult.Fail("Unauthorized");
            }

            try
            {
                return await this.ValidateToken(token);
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex.Message);
            }
        }

        private async Task<AuthenticateResult> ValidateToken(string token)
        {
            var _UserToken = this.m_PersistenceContext.GetEntities<UserToken>()
                .Where(ut => ut.AccessToken == token)
                .SingleOrDefault();

            if (_UserToken == null || string.IsNullOrEmpty(_UserToken.AccessToken))
                return AuthenticateResult.Fail("Unauthorized");

            var _Claims = new List<Claim> {
                new Claim(ClaimTypes.Name, _UserToken.AccessToken),
                new Claim(ClaimTypes.NameIdentifier, _UserToken.AccountID.ToString())
            };

            var _Identity = new ClaimsIdentity(_Claims, Scheme.Name);
            var _Principal = new System.Security.Principal.GenericPrincipal(_Identity, null);
            var _Ticket = new AuthenticationTicket(_Principal, Scheme.Name);
            return AuthenticateResult.Success(_Ticket);
        }
    }

}
