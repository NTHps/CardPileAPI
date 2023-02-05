namespace CardPile.Application.Services.OAuth
{

    public class OAuthOptions
    {

        #region - - - - - - Properties - - - - - -

        public int AccessTokenTimeToLiveSeconds { get; set; }

        public int RefreshTokenTimeToLiveSeconds { get; set; }

        #endregion Properties

    }

}
