using AutoMapper;
using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardInteractor : IUseCaseInteractor<CreateCardInputPort, ICreateCardOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCardInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(CreateCardInputPort inputPort, ICreateCardOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Card = this.m_Mapper.Map<Card>(inputPort);
            this.m_PersistenceContext.Add(_Card);
            return outputPort.PresentCreatedCardAsync(this.m_Mapper.Map<CreatedCardDto>(_Card), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
