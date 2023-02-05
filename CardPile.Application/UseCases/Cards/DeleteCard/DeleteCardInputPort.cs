using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.DeleteCard
{

    public class DeleteCardInputPort : IUseCaseInputPort<IDeleteCardOutputPort>
    {

        #region - - - - - - Properties - - - - - -

        public long CardID { get; set; }

        #endregion Properties

    }

}
