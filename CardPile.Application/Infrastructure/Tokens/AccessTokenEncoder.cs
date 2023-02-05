using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Services.OAuth.Tokens;
using CardPile.Application.Services.Tokens;

namespace CardPile.Application.Infrastructure.Tokens
{

    public class AccessTokenEncoder : ITokenEncoder<AccessTokenClaims, AccessTokenSecretClaims>
    {

        #region - - - - - - Fields - - - - - -

        private readonly ITokeniser m_Tokeniser;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public AccessTokenEncoder(ITokeniser tokeniser)
        {
            this.m_Tokeniser = tokeniser ?? throw CodeContract.ArgumentNullException(nameof(tokeniser));
        }

        #endregion Constructors

        #region - - - - - - ITokenEncoder Implementation - - - - - -

        public string Encode(TokenComponents<AccessTokenClaims, AccessTokenSecretClaims> tokenComponents) => this.m_Tokeniser.Tokenise(tokenComponents);

        #endregion ITokenEncoder Implementation

    }

}
