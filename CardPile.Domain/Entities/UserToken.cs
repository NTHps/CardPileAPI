namespace CardPile.Domain.Entities
{

    public class UserToken
    {

        #region - - - - - - Fields - - - - - -

        public const string DefaultAuthenticationScheme = "Bearer";
        public const string PasswordAuthenticationScheme = "Signet";

        #endregion Fields

        #region - - - - - - Properties - - - - - -

        public string AccessToken { get; set; }
        public int ExpiresIn { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public string Username { get; set; }

        #endregion Properties

    }

}
