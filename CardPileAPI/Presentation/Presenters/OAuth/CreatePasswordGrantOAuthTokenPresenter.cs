using AutoMapper;
using CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken;
using CardPileAPI.Presentation.ApiResponse.OAuth;

namespace CardPileAPI.Presentation.Presenters.OAuth
{

    public class CreatePasswordGrantOAuthTokenPresenter : BasePresenter, ICreatePasswordGrantOAuthTokenOutputPort
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CreatePasswordGrantOAuthTokenPresenter(IMapper mapper) : base(mapper)
            => this.m_Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public Task PresentAccessToken(CreatePasswordGrantOAuthTokenResponse response, CancellationToken cancellationToken)
        {
            this.PresentedSuccessfully = true;
            return this.CreatedAsync(() => this.m_Mapper.Map<CreatePasswordGrantOAuthTokenApiResponse>(response));
        }

        #endregion Methods

    }

}
