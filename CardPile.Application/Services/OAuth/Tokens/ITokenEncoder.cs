namespace CardPile.Application.Services.OAuth.Tokens
{

    public interface ITokenEncoder<TClaims, TSecretClaims>
    {

        string Encode(TokenComponents<TClaims, TSecretClaims> tokenComponents);

    }

}
