using CleanArchitecture.Mediator;
using Newtonsoft.Json;

namespace CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken
{

    public class CreatePasswordGrantOAuthTokenInputPort : IUseCaseInputPort<ICreatePasswordGrantOAuthTokenOutputPort>
    {

        #region - - - - - - Properties - - - - - -

        [JsonProperty("client_id")]
        public long ClientID { get; set; }

        [JsonProperty("client_secret")]
        public string ClientSecret { get; set; }

        [JsonProperty("grant_type")]
        public string GrantType { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }

        #endregion Properties

    }

}
