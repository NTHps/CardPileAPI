using Newtonsoft.Json;

namespace CardPileAPI.Presentation.Commands.OAuth
{

    public class OAuthCommand
    {

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The id given to accepted clients of the Liink Api
        /// </summary>
        [JsonProperty("client_id")]
        public long ClientID { get; set; }

        /// <summary>
        /// A string given to accepted clients of the Liink Api
        /// </summary>
        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        /// <summary>
        /// The type of OAuth2.0 Grant you are requesting
        /// </summary>
        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        /// <summary>
        /// Password of the user. Used in OAuth2.0 password grant
        /// </summary>
        [JsonProperty("password")]
        public string Password { get; set; }

        /// <summary>
        /// Token used to refresh an expired Access Token within the refresh token's lifespan
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }


        /// <summary>
        /// The scopes you are requesting in your grant. If left empty (not null), it will grant you all scopes your grant and authentication has access to
        /// </summary>
        [JsonProperty("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// Username of the user. Used in OAuth2.0 password grant
        /// </summary>
        [JsonProperty("username")]
        public string Username { get; set; }

        #endregion Properties

    }

}
