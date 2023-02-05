using CleanArchitecture.Mediator;

namespace CardPile.Application.UseCases.Accounts.RegisterAccount
{

    public class RegisterAccountInputPort : IUseCaseInputPort<IRegisterAccountOutputPort>
    {

        #region - - - - - - Properties - - - - - -

        public string Email { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }

        #endregion Properties

    }

}
