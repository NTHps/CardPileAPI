using CardPile.Domain.ValueObjects;

namespace CardPile.Domain.Entities
{

    public class Account
    {

        #region - - - - - - Properties - - - - - -

        public long AccountID { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public string Email { get; set; }
        public Guid? GuestToken { get; set; }
        public Password Password { get; set; }

        #endregion Properties

    }

}
