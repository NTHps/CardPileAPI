using CardPile.Application.Infrastructure.Validation;
using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;
using FluentValidation.Results;

namespace CardPile.Application.UseCases.Cards.DeleteCard
{

    public class DeleteCardBusinessRuleValidator : IUseCaseBusinessRuleValidator<DeleteCardInputPort, CleanValidationResult>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteCardBusinessRuleValidator(IPersistenceContext persistenceContext)
            => this.m_PersistenceContext = persistenceContext ?? throw new System.ArgumentNullException(nameof(persistenceContext));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task<CleanValidationResult> ValidateAsync(DeleteCardInputPort inputPort, CancellationToken cancellationToken)
            => this.m_PersistenceContext.GetEntities<DeckList>().Where(ri => ri.Cards.Any(c => c.CardID == inputPort.CardID)).Any()
                        ? Task.FromResult(new CleanValidationResult(new List<ValidationFailure>() { new ValidationFailure(string.Empty, "You cannot delete a Card that is being used in a Deck.") }))
                        : Task.FromResult(new CleanValidationResult(new List<ValidationFailure>()));

        #endregion Methods

    }

}
