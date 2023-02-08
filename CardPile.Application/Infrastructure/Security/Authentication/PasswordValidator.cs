using CardPile.Application.Common.CodeContractgs;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Application.Services.Persistence;
using CardPile.Application.Services.Security.Authentication;
using CardPile.Domain.Entities;
using CardPile.Domain.ValueObjects;
using Microsoft.AspNet.Identity;

namespace CardPile.Application.Infrastructure.Security.Authentication
{

    public class PasswordValidator : IPasswordValidator
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public PasswordValidator(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw CodeContract.ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - IPasswordValidator Implementation - - - - - -

        public async Task ValidatePasswordAsync(Account account, string plainTextPassword, CancellationToken cancellationToken)
        {
            switch (account.Password.VerifyHash(plainTextPassword))
            {
                case PasswordVerificationResult.Failed:
                    throw new OAuthException(OAuthErrorValuesEnum.invalid_grant, "Invalid Username or Password.");
                case PasswordVerificationResult.SuccessRehashNeeded:
                    {
                        account.Password.RehashKey(plainTextPassword);
                        _ = await this.m_PersistenceContext.SaveChangesAsync(cancellationToken);
                    }
                    break;
                case PasswordVerificationResult.Success:
                    {
                        account.Password = new Password(plainTextPassword);
                        _ = await this.m_PersistenceContext.SaveChangesAsync(cancellationToken);
                    }
                    break;
            }
        }

        #endregion IPasswordValidator Implementation

    }

}
