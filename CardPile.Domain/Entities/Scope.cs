namespace CardPile.Domain.Entities
{

    public class Scope
    {

        #region - - - - - - Properties - - - - - -

        public long ScopeID { get; set; }

        public string Name { get; set; }


        // Navigation Properties ---------------------------------------------------------

        public ICollection<ClientApplicationScope> ClientApplicationScopes { get; set; }

        #endregion Properties

    }

}
