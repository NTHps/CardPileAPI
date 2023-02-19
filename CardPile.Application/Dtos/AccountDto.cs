namespace CardPile.Application.Dtos
{

    public class AccountDto
    {

        #region - - - - - - Properties - - - - - -

        public long AccountID { get; set; }
        public Guid? GuestToken { get; set; }
        public string UserName { get; set; }

        #endregion Properties

    }

}
