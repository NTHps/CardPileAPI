using CardPile.Application.Dtos;
using CardPile.Application.Infrastructure.Validation;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.DeleteCard
{

    public interface IDeleteCardOutputPort : IAuthenticationOutputPort, IBusinessRuleValidationOutputPort<CleanValidationResult>, IValidationOutputPort<CleanValidationResult>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentDeletedCardAsync(CardDto card, CancellationToken cancellationToken);

        Task PresentCardNotFound(long cardID, CancellationToken cancellationToken);

        #endregion Methods

    }

}
