using CardPile.Application.Infrastructure.Validation;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public interface ICreateCardOutputPort :
        IAuthenticationOutputPort,
        IValidationOutputPort<CleanValidationResult>
    {

        #region - - - - - - Methods - - - - - -

        Task PresentCreatedCardAsync(CreatedCardDto card, CancellationToken cancellationToken);

        #endregion Methods

    }

}
