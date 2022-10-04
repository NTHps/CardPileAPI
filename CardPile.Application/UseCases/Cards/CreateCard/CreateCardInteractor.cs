using AutoMapper;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardInteractor : IUseCaseInteractor<CreateCardInputPort, ICreateCardOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCardInteractor(IMapper mapper)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(CreateCardInputPort inputPort, ICreateCardOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Card = this.m_Mapper.Map<Card>(inputPort);
            _Card.CardID = 1;
            //this.m_PersistenceContext.Add(_Recipe);

            return outputPort.PresentCreatedCardAsync(this.m_Mapper.Map<CreatedCardDto>(_Card), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
