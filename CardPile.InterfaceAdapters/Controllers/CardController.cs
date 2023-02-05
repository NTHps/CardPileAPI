using CardPile.Application.UseCases.Cards.CreateCard;
using CardPile.Application.UseCases.Cards.DeleteCard;
using CleanArchitecture.Mediator;

namespace CardPile.InterfaceAdapters.Controllers
{

    public class CardController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CardController(IUseCaseInvoker useCaseInvoker)
            => this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task CreateCardAsync(CreateCardInputPort inputPort, ICreateCardOutputPort outputPort, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(inputPort, outputPort, cancellationToken);

        public Task DeleteCardAsync(DeleteCardInputPort inputPort, IDeleteCardOutputPort outputPort, CancellationToken cancellationToken)
           => this.m_UseCaseInvoker.InvokeUseCaseAsync(inputPort, outputPort, cancellationToken);

        #endregion Methods

    }

}
