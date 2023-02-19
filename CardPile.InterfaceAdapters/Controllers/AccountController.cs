using CardPile.Application.UseCases.Accounts.RegisterAccount;
using CleanArchitecture.Mediator;

namespace CardPile.InterfaceAdapters.Controllers
{

    public class AccountController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public AccountController(IUseCaseInvoker useCaseInvoker)
            => this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task RegisterAccountAsync(RegisterAccountInputPort inputPort, IRegisterAccountOutputPort outputPort, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(inputPort, outputPort, cancellationToken);

        #endregion Methods

    }

}
