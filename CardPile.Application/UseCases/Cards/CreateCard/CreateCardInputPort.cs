using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardInputPort : IUseCaseInputPort<ICreateCardOutputPort>
    {

        #region - - - - - - Properties - - - - - -

        public string Name { get; set; } = "";

        #endregion Properties

    }

}
