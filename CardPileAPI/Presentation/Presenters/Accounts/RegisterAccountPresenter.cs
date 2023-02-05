using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.UseCases.Accounts.RegisterAccount;
using CardPileAPI.Presentation.ViewModels.Accounts.RegisterAccount;

namespace CardPileAPI.Presentation.Presenters.Accounts
{

    public class RegisterAccountPresenter : BasePresenter, IRegisterAccountOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public RegisterAccountPresenter(IMapper mapper) : base(mapper)
            => this.m_Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task PresentAccountRegisteredAsync(AccountDto account, CancellationToken cancellationToken)
        {
            this.PresentedSuccessfully = true;
            return this.CreatedAsync(() => this.m_Mapper.Map<RegisterAccountResponse>(account));
        }

        #endregion Methods

    }

}
