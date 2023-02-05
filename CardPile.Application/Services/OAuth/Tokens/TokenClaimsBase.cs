﻿using CardPile.Application.Infrastructure.Authentication.OAuth;

namespace CardPile.Application.Services.OAuth.Tokens
{

    public abstract class TokenClaimsBase
    {

        #region - - - - - - Constructors - - - - - -

        public TokenClaimsBase()
        {

        }

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public long TicksWhenCreated { get; set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        /// <summary>
        /// If the Token has an Account ID claim, return it.
        /// </summary>
        /// <returns>An Account ID if the claim is present. Otherwise, null.</returns>
        public abstract long? GetAccountIDClaim();

        /// <summary>
        /// If the Token has a Client Application ID claim, return it.
        /// </summary>
        /// <returns>A Client Application ID if the claim is present. Otherwise, null.</returns>
        public abstract long? GetClientApplicationIDClaim();

        /// <summary>
        /// If the Token has a Client Application Name claim, return it.
        /// </summary>
        /// <returns></returns>
        public abstract string GetClientApplicationNameClaim();

        /// <summary>
        /// Return a collection containing any metadata the token contains.
        /// </summary>
        /// <returns></returns>
        public abstract ICollection<MetaData> GetMetaDataClaim();

        /// <summary>
        /// If the Token has a Scope claim, return it.
        /// </summary>
        /// <returns></returns>
        public abstract string GetScopeClaim();

        #endregion Methods

    }

}
