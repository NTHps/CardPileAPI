namespace CardPile.Domain.Entities
{

    public class GrantType
    {

        #region - - - - - - Constructors - - - - - -

        public GrantType()
        {
            this.ClientApplicationGrantTypes = new HashSet<ClientApplicationGrantType>();
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public long GrantTypeID { get; set; }
        public string Grant { get; set; }

        // Navigation Properties ---------------------------------------------------------

        public ICollection<ClientApplicationGrantType> ClientApplicationGrantTypes { get; set; }

        #endregion Properties

    }

}
