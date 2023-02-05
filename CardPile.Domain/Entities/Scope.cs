namespace CardPile.Domain.Entities
{

    public class Scope
    {

        #region - - - - - - Constructors - - - - - -

        public Scope()
        {
            this.ClientApplicationScopes = new HashSet<ClientApplicationScope>();
        }

        #endregion

        #region - - - - - - Properties - - - - - -

        public long ScopeID { get; set; }
        public string Name { get; set; }


        // Navigation Properties ---------------------------------------------------------

        public ICollection<ClientApplicationScope> ClientApplicationScopes { get; set; }

        #endregion

    }

}
