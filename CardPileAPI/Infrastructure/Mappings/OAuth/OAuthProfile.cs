using AutoMapper;
using CardPile.Application.UseCases.OAuth.CreatePasswordGrantOAuthToken;
using CardPileAPI.Presentation.ApiResponse.OAuth;
using CardPileAPI.Presentation.Commands.OAuth;

namespace CardPileAPI.Infrastructure.Mappings.OAuth
{

    public class OAuthProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public OAuthProfile()
        {
            _ = this.CreateMap<OAuthCommand, CreatePasswordGrantOAuthTokenInputPort>();

            _ = this.CreateMap<CreatePasswordGrantOAuthTokenResponse, CreatePasswordGrantOAuthTokenApiResponse>();
        }

        #endregion Constructors

    }

}
