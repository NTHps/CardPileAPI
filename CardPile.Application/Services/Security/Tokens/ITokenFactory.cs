using CardPile.Domain.Entities;

namespace CardPile.Application.Services.Security.Tokens
{

    public interface ITokenFactory
    {

        UserToken GetToken(ClientApplication clientApplication, IEnumerable<Scope> scope);

    }

}
