using CardPile.Application.Services.OAuth.Tokens;

namespace CardPile.Application.Services.Tokens
{

    public interface ITokeniser
    {

        string Tokenise<TClaims, TSecretClaims>(TokenComponents<TClaims, TSecretClaims> tokenComponents) where TClaims : TokenClaimsBase;

        TClaims DeTokenise<TClaims>(string token) where TClaims : TokenClaimsBase;

    }

}
s