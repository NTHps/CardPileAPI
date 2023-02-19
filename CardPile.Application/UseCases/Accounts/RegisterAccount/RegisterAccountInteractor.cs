using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.Services.Persistence;
using CardPile.Domain.Entities;
using CardPile.Domain.ValueObjects;
using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Accounts.RegisterAccount
{

    public class RegisterAccountInteractor : IUseCaseInteractor<RegisterAccountInputPort, IRegisterAccountOutputPort>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;
        private readonly IPersistenceContext m_PersistenceContext;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public RegisterAccountInteractor(IMapper mapper, IPersistenceContext persistenceContext)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.m_PersistenceContext = persistenceContext ?? throw new ArgumentNullException(nameof(persistenceContext));
        }

        #endregion Constructors

        #region - - - - - - IUseCaseInteractor Implementation - - - - - -

        public async Task HandleAsync(RegisterAccountInputPort inputPort, IRegisterAccountOutputPort outputPort, CancellationToken cancellationToken)
        {
            var _Account = new Account()
            {
                UserName = inputPort.UserName,
                CreatedOnUTC = DateTime.UtcNow,
                Password = new Password(inputPort.Password)
            };
            if (string.IsNullOrEmpty(inputPort.Email))
                _Account.GuestToken = Guid.NewGuid();
            else
                _Account.Email = inputPort.Email;


            this.m_PersistenceContext.Add(_Account);
            await this.m_PersistenceContext.SaveChangesAsync(cancellationToken);
            await outputPort.PresentAccountRegisteredAsync(this.m_Mapper.Map<AccountDto>(_Account), cancellationToken);
        }

        #endregion IUseCaseInteractor Implementation

    }

}
