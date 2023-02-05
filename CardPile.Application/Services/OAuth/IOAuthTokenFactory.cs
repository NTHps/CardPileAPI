using CardPile.Application.Infrastructure.Authentication.OAuth;
using CardPile.Domain.Entities;
1
namespace CardPile.Application.Services.OAuth
{

    public interface IOAuthTokenFactory
    {

        OAuthToken GetToken(ClientApplication clientApplication, IEnumerable<Scope> scope);
        OAuthToken GetToken(Account account, ClientApplication clientApplication, IEnumerable<Scope> scope, ICollection<MetaData> metaData);
        string GetToken();

    }

}
