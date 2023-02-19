using AutoMapper;
using CardPile.Application.Dtos;
using CardPile.Application.UseCases.Accounts.RegisterAccount;
using CardPileAPI.Presentation.Commands.Accounts;
using CardPileAPI.Presentation.ViewModels.Accounts.RegisterAccount;

namespace CardPileAPI.Infrastructure.Mappings.Accounts
{

    public class AccountsProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public AccountsProfile()
        {
            // Register
            _ = this.CreateMap<RegisterCompanyCommand, RegisterAccountInputPort>();
            _ = this.CreateMap<AccountDto, RegisterAccountResponse>();

        }

        #endregion Constructors

    }

}
