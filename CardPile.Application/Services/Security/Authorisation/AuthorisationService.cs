using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace CardPile.Application.Services.Security.Authorisation
{

    public class AuthorisationService : IAuthorisationService
    {

        #region - - - - - - Fields - - - - - -

        private readonly IHttpContextAccessor m_HttpContextAccessor;

        #endregion

        #region - - - - - - Constructors - - - - - -

        public AuthorisationService(IHttpContextAccessor httpContextAccessor)
        {
            this.m_HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        #endregion

        #region - - - - - - Properties - - - - - -

        public ClaimsPrincipal User => this.m_HttpContextAccessor.HttpContext.User;

        #endregion

        #region - - - - - - Methods - - - - - -

        public AuthenticationMetadata GetAuthenticationMetadata()
        {
            if (this.User == null)
                return null;

            // Attempt to extract the ID
            var _AccountID = this.GetFirstValueOrDefault<long?>(ClaimTypes.NameIdentifier);

            return new AuthenticationMetadata(_AccountID, null, null, null);
        }

        public T GetFirstValueOrDefault<T>(string claimType)
        {
            if (this.User == null || string.IsNullOrWhiteSpace(claimType))
                return default(T);

            // Attempt to retrieve the first instance of the claim type.
            var _ClaimValue = this.User.FindFirst(claimType)?.Value;

            // Return the default value if the claim type is not present or the value is empty.
            if (string.IsNullOrEmpty(_ClaimValue))
                return default(T);

            // Get the underlying type.
            var _Type = typeof(T);
            if (_Type.IsGenericType && _Type.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                _Type = Nullable.GetUnderlyingType(_Type);

            return (T)Convert.ChangeType(_ClaimValue, _Type);
        }

        #endregion Methods

    }

}
