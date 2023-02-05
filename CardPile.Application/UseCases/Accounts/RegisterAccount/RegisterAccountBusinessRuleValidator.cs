using CardPile.Application.Infrastructure.Validation;
using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using FluentValidation.Results;

namespace CardPile.Application.UseCases.Accounts.RegisterAccount
{

    public class RegisterAccountBusinessRuleValidator
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public RegisterAccountBusinessRuleValidator(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new System.ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task<CleanValidationResult> ValidateAsync(RegisterAccountInputPort inputPort, CancellationToken cancellationToken)
            => this.m_PersistenceContext.GetEntities<Account>().Any(ri => ri.UserName == inputPort.UserName || ri.Email == inputPort.Email)
                        ? Task.FromResult(new CleanValidationResult(new List<ValidationFailure>() { new ValidationFailure(string.Empty, "An account with this user name / email is already in use.") }))
                        : Task.FromResult(new CleanValidationResult(new List<ValidationFailure>()));

        #endregion Methods

    }

}
