namespace CardPile.Application.Services.Security.Tokens
{

    public class AccessTokenClaims : TokenClaimsBase
    {

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The Account ID.
        /// </summary>
        public long AccountID { get; set; }

        /// <summary>
        /// The Client Application ID.
        /// </summary>
        public long ClientID { get; set; }

        /// <summary>
        /// The Client Application Name
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The Comma-separated Scope IDs.
        /// </summary>
        public string ScopeIDs { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public override long? GetAccountIDClaim() => this.AccountID;

        public override long? GetClientApplicationIDClaim() => this.ClientID;

        public override string GetClientApplicationNameClaim() => this.ClientName;

        public override string GetScopeClaim() => this.ScopeIDs;

        #endregion Methods

    }

    public class AccessTokenSecretClaims
    {

        #region - - - - - - Properties - - - - - -

        /// <summary>
        /// The Account's Email.
        /// </summary>
        public string AccountEmail { get; set; }

        /// <summary>
        ///  The Client Application's Name.
        /// </summary>
        public string ClientName { get; set; }

        /// <summary>
        /// The Comma-separated Scope Names.
        /// </summary>
        public string ScopeNames { get; set; }

        public long TicksWhenCreated => DateTime.UtcNow.Ticks;

        /// <summary>
        /// The Type of Token.
        /// </summary>
        public string TokenType => TokenTypes.AccessToken;

        #endregion Properties

    }

}
