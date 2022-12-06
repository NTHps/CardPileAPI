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

        public async Task<CleanValidationResult> ValidateAsync(DeleteCardInputPort inputPort, CancellationToken cancellationToken)
        {
            var _Decks = this.m_PersistenceContext.GetEntities<DeckList>();
            if (_Decks.Any(deck => deck.Cards.Any(c => c.CardID == inputPort.CardID)))
                return new CleanValidationResult(new List<ValidationFailure>() { new ValidationFailure(string.Empty, "You cannot delete an Ingredient that is being used by a recipe.") });
            else
                return new CleanValidationResult(new List<ValidationFailure>());

            // this is completely wrong, fix when concious 
        }

        #endregion Methods

    }

}
