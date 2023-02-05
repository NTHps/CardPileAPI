using CardPile.Application.Infrastructure.Authentication.OAuth;

namespace CardPileAPI.Presentation.ViewModels.OAuth
{

    public class OAuthViewModel
    {

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The token used to accesss the Liink Api as a authenticated user
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// The logged in user's Account ID
        /// </summary>
        public long AccountID { get; set; }

        /// <summary>
        /// The time in seconds until the Assecc Token expires
        /// </summary>
        public string ExpiresIn { get; set; }

        /// <summary>
        /// The type of OAuth2.0 Grant granted
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// List of metadata about the logged in user
        /// </summary>
        public List<MetaData> MetaData { get; set; }

        /// <summary>
        /// Token used to refresh an expired Access Token within the refresh token's lifespan
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Scopes granted
        /// </summary>
        public string Scope { get; set; }

        #endregion Properties

    }
1
}
