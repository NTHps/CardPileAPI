using System.Security.Claims;

namespace CardPile.Application.Services.Security.Authorisation
{

    public interface IAuthorisationService
    {

        ClaimsPrincipal User { get; }

        /// <summary>
        /// Given a ClaimsPrincipal, return an AuthenticationMetadata object.
        /// </summary>
        /// <returns></returns>
        AuthenticationMetadata GetAuthenticationMetadata();

        /// <summary>
        /// Given a ClaimsPrincipal and a Claim Type, convert the first value to the specified type, or return its default.
        /// </summary>
        /// <typeparam name="T">The type to convert the Claim Type value to.</typeparam>
        /// <param name="claimType">The Claim Type to find.</param>
        /// <returns>The value converted to the type specified if found, otherwise default.</returns>
        T GetFirstValueOrDefault<T>(string claimType);

    }

}
