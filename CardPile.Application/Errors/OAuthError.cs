using Newtonsoft.Json;

namespace CardPile.Application.Errors
{

    public class OAuthError
    {

        #region - - - - - - Constructors - - - - - -

        public OAuthError(string error, string errorDescription)
        {
            this.Error = error;
            this.ErrorDescription = errorDescription;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        [JsonProperty("error")]
        public string Error { get; }

        [JsonProperty("error_description")]
        public string ErrorDescription { get; }

        #endregion Properties

    }

}
