using AutoMapper;
using CardPile.Application.UseCases.Cards.CreateCard;
using CardPileAPI.Presentation.ViewModels.Cards;

namespace CardPileAPI.Presentation.Presenters.Cards
{

    public class CreateCardPresenter : BasePresenter, ICreateCardOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreateCardPresenter(IMapper mapper) : base(mapper)
            => this.m_Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task PresentCreatedCardAsync(CreateCardDto ingredient, CancellationToken cancellationToken)
        {
            this.PresentedSuccessfully = true;
            return this.CreatedAsync(() => this.m_Mapper.Map<CardViewModel>(ingredient));
        }

        #endregion Methods

    }

}
