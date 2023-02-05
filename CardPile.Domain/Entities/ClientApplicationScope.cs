namespace CardPile.Domain.Entities
{

    public class ClientApplicationScope
    {

        #region - - - - - - Properties - - - - - -

        public long ClientApplicationID { get; set; }
        public long ScopeID { get; set; }


        // Navigation Properties ---------------------------------------------------------

        public ClientApplication ClientApplication { get; set; }
        public Scope Scope { get; set; }

        #endregion

    }

}
