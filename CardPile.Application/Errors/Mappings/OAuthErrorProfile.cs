using AutoMapper;
using CardPile.Application.Exceptions;

namespace CardPile.Application.Errors.Mappings
{
    public class OAuthErrorProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public OAuthErrorProfile()
        {
            this.CreateMap<OAuthException, OAuthError>().ConvertUsing((oAuthException, ae)
                => new OAuthError(oAuthException.Error, oAuthException.ErrorDescription));
        }

        #endregion Constructors
    }

}
