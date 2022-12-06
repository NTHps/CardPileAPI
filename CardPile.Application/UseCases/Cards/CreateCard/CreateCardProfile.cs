using AutoMapper;
using CardPile.Domain.Entities;

namespace CardPile.Application.UseCases.Cards.CreateCard
{

    public class CreateCardProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CreateCardProfile()
        {
            _ = this.CreateMap<CreateCardInputPort, Card>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.CardID, o => o.Ignore());

            _ = this.CreateMap<Card, CreatedCardDto>();
        }

        #endregion Constructors

    }

}
