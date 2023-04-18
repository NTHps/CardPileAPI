namespace CardPile.Application.Services.Security
{

    public class AuthenticationMetadata
    {

        #region - - - - - - Constructors - - - - - -

        public AuthenticationMetadata(long? accountID, long? clientApplicationID, string clientName, string scopes)
        {
            this.AccountID = accountID;
            this.ClientApplicationID = clientApplicationID;
            this.ClientName = clientName;
            this.Scopes = scopes;
        }

        public AuthenticationMetadata(long? clientApplicationID, string clientName, string scopeNames)
        {
            this.ClientApplicationID = clientApplicationID;
            this.ClientName = clientName;
            this.Scopes = scopeNames;
        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The Account's ID.
        /// </summary>
        public long? AccountID { get; }

        /// <summary>
        /// The Client Application's ID.
        /// </summary>
        public long? ClientApplicationID { get; }

        /// <summary>
        /// The Client Application Name
        /// </summary>
        public string ClientName { get; }

        /// <summary>
        /// The comma-separated scope names.
        /// </summary>
        public string Scopes { get; }

        #endregion Properties

    }

}
