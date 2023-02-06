namespace CardPileAPI.Presentation.ApiResponse.OAuth
{

    public class CreatePasswordGrantOAuthTokenApiResponse
    {

        #region - - - - - - Properties - - - - - -

        public string AccessToken { get; set; }
        public long AccountID { get; set; }
        public string ExpiresIn { get; set; }
        public string GrantType { get; set; }
        public string RefreshToken { get; set; }
        public string Scope { get; set; }

        #endregion Properties

    }

}
