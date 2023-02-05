using AutoMapper;
using CardPile.Application.Services.Persistence;
using CardPile.Application.UseCases.Accounts.RegisterAccount;
using CardPile.InterfaceAdapters.Controllers;
using CardPileAPI.Presentation.Commands.Accounts;
using CardPileAPI.Presentation.Presenters.Accounts;
using CardPileAPI.Presentation.ViewModels.Accounts.RegisterAccount;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardPileAPI.Controllers
{

    public class AccountsController : BaseController
    {

        #region - - - - - - Fields - - - - - -

        private readonly AccountController m_AccountsInterfaceAdapter;
        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public AccountsController(AccountController accountsController, IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_AccountsInterfaceAdapter = accountsController ?? throw new ArgumentNullException(nameof(accountsController));
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }
        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        [HttpPost]
        [ProducesResponseType(typeof(RegisterAccountResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(ValidationProblemDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RegisterAccount([FromBody] RegisterCompanyCommand command)
        {
            var _Presenter = new RegisterAccountPresenter(this.m_Mapper);

            await this.m_AccountsInterfaceAdapter.RegisterAccountAsync(this.m_Mapper.Map<RegisterAccountInputPort>(command), _Presenter, CancellationToken.None);

            return _Presenter.Result;
        }

        #endregion Methods

    }

}
