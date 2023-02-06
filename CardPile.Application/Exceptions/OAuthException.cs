using CardPile.Application.Errors;

namespace CardPile.Application.Exceptions
{

    public class OAuthException : Exception
    {

        #region - - - - - - Constructors - - - - - -

        public OAuthException(OAuthErrorValuesEnum authError)
        {
            this.Error = authError.ToString() ?? "";
            this.ErrorDescription = "";
        }

        public OAuthException(OAuthErrorValuesEnum authError, string description)
        {
            this.Error = authError.ToString() ?? "";
            this.ErrorDescription = description ?? "";
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public string Error { get; }
        public string ErrorDescription { get; }

        #endregion Properties

    }

}
