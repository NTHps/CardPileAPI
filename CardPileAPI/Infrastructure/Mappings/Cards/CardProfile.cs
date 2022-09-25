﻿using AutoMapper;
using CardPile.Application.UseCases.Cards.CreateCard;
using CardPileAPI.Presentation.Commands.Cards;
using CardPileAPI.Presentation.ViewModels.Cards;

namespace CardPileAPI.Infrastructure.Mappings.Cards
{

    public class CardProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public CardProfile()
        {
            _ = this.CreateMap<CreateCardCommand, CreateCardInputPort>();

            _ = this.CreateMap<CreateCardDto, CardViewModel>()
                    .ForMember(dest => dest.CardID, opts => opts.MapFrom(src => src.CardID.Invoke()))
                    .ForMember(dest => dest.CardName, opts => opts.MapFrom(src => src.Name));
        }

        #endregion Constructors

    }

}
