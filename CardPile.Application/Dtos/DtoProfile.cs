using AutoMapper;
using CardPile.Domain.Entities;

namespace CardPile.Application.Dtos
{

    public class DtoProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public DtoProfile()
        {
            {
                _ = this.CreateMap<CardDto, Card>();
                _ = this.CreateMap<Card, CardDto>();
            }

            #endregion Constructors

        }

    }

}
