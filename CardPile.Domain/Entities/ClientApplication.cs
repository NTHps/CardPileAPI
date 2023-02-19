namespace CardPile.Domain.Entities
{

    public class ClientApplication
    {

        #region - - - - - - Properties - - - - - -

        public long ClientApplicationID { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public string AccessToken { get; set; }

        #endregion Properties

    }

}
