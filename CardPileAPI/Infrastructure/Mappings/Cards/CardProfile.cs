using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.UseCases.Cards.CreateCard;
using CardPile.Application.UseCases.Cards.DeleteCard;
using CardPileAPI.Presentation.Commands.Cards;
using CardPileAPI.Presentation.ViewModels.Cards;

namespace CardPileAPI.Infrastructure.Mappings.Cards
{

    public class CardProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CardProfile()
        {
            // Create
            _ = this.CreateMap<CreateCardCommand, CreateCardInputPort>();

            _ = this.CreateMap<CreatedCardDto, CardViewModel>()
                    .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

            _ = this.CreateMap<CardDto, CardViewModel>()
                   .ForMember(dest => dest.Name, opts => opts.MapFrom(src => src.Name));

            // Delete
            _ = this.CreateMap<DeleteCardCommand, DeleteCardInputPort>();
        }

        #endregion Constructors

    }

}
