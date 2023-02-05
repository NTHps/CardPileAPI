namespace CardPile.Application.Services.OAuth.Tokens
{

    public class TokenComponents<TClaims, TSecretClaims>
    {

        #region - - - - - - Constructors - - - - - -

        public TokenComponents(TClaims claims, TSecretClaims secretClaims)
        {
            this.Claims = claims;
            this.SecretClaims = secretClaims;
        }

        #endregion

        #region - - - - - - Properties - - - - - -

        public TClaims Claims { get; }

        public TSecretClaims SecretClaims { get; }

        #endregion

    }

}
