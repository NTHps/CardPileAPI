using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.UseCases.Cards.DeleteCard;
using CardPile.Domain.Entities;
using CardPileAPI.Presentation.ViewModels.Cards;

namespace CardPileAPI.Presentation.Presenters.Cards
{

    public class DeleteCardPresenter : BasePresenter, IDeleteCardOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public DeleteCardPresenter(IMapper mapper) : base(mapper)
            => this.m_Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task PresentDeletedCardAsync(CardDto card, CancellationToken cancellationToken)
        {
            this.PresentedSuccessfully = true;
            return this.OkAsync(this.m_Mapper.Map<CardViewModel>(card));
        }

        public Task PresentCardNotFound(long carddID, CancellationToken cancellationToken)
            => this.NotFoundRouteAsync(nameof(Card), carddID);

        #endregion Methods

    }

}
