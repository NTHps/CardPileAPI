namespace CardPileAPI.Presentation.ViewModels.Accounts.RegisterAccount
{

    public class RegisterAccountResponse
    {

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The ID of the Account.
        /// </summary>
        public long AccountID { get; set; }

        /// <summary>
        /// If the user is a guest, this is the their token
        /// </summary>
        public Guid? GuestToken { get; set; }

        /// <summary>
        /// The Name of the Account.
        /// </summary>
        public string UserName { get; set; } = "";

        #endregion Properties

    }

}
