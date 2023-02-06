using CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken;
using CleanArchitecture.Mediator;

namespace CardPile.InterfaceAdapters.Controllers
{

    public class OAuthIAController
    {

        #region - - - - - - Fields - - - - - -

        private readonly IUseCaseInvoker m_UseCaseInvoker;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public OAuthIAController(IUseCaseInvoker useCaseInvoker)
            => this.m_UseCaseInvoker = useCaseInvoker ?? throw new ArgumentNullException(nameof(useCaseInvoker));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task CreatePasswordGrantOAuthToken(CreatePasswordGrantOAuthTokenInputPort inputPort, ICreatePasswordGrantOAuthTokenOutputPort outputPort, CancellationToken cancellationToken)
            => this.m_UseCaseInvoker.InvokeUseCaseAsync(inputPort, outputPort, cancellationToken);

        #endregion Methods

    }

}
