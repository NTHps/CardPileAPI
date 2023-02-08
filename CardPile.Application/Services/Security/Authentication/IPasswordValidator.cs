using CardPile.Domain.Entities;

namespace CardPile.Application.Services.Security.Authentication
{

    public interface IPasswordValidator
    {

        Task ValidatePasswordAsync(Account account, string plainTextPassword, CancellationToken cancellationToken);

    }

}
