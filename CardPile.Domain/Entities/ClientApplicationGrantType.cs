namespace CardPile.Domain.Entities
{

    public class ClientApplicationGrantType
    {

        #region - - - - - - Properties - - - - - -

        public long ClientApplicationID { get; set; }
        public long GrantTypeID { get; set; }

        // Navigation Properties ---------------------------------------------------------

        public ClientApplication ClientApplication { get; set; }
        public GrantType GrantType { get; set; }

        #endregion Properties

    }

}
