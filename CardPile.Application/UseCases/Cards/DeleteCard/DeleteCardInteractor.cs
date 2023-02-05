using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Cards.DeleteCard
{

    public class DeleteCardInteractor : IUseCaseInteractor<DeleteCardInputPort, IDeleteCardOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteCardInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public Task HandleAsync(DeleteCardInputPort inputPort, IDeleteCardOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Card = this.m_PersistenceContext.Find<Card>(new object[] { inputPort.CardID });

            if (_Card == null)
                return outputPort.PresentCardNotFound(inputPort.CardID, cancellationToken);

            this.m_PersistenceContext.Remove(_Card);

            return outputPort.PresentDeletedCardAsync(this.m_Mapper.Map<CardDto>(_Card), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
