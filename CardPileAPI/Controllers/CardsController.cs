using AutoMapper;
using CardPile.Application.Services.Persistence;
using CardPile.Application.UseCases.Cards.CreateCard;
using CardPile.InterfaceAdapters.Controllers;
using CardPileAPI.Presentation.Commands.Cards;
using CardPileAPI.Presentation.Presenters.Cards;
using CardPileAPI.Presentation.ViewModels.Cards;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardPileAPI.Controllers
{

    public class CardsController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly CardController m_CardController;
        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CardsController(CardController cardController, IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_CardController = cardController ?? throw new ArgumentNullException(nameof(cardController));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        [HttpPost]
        [ProducesResponseType(typeof(CardViewModel), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateCard([FromBody] CreateCardCommand command)
        {
            var _Presenter = new CreateCardPresenter(this.m_Mapper);

            await this.m_CardController.CreateCardAsync(this.m_Mapper.Map<CreateCardInputPort>(command), _Presenter, CancellationToken.None);

            if (_Presenter.PresentedSuccessfully)
                await this.m_PersistenceContext.SaveChangesAsync(CancellationToken.None);

            return _Presenter.Result;
        }

        #endregion Methods

    }

}
