using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CardPileAPI.Infrastructure.ModelBinding
{

    public class BodyAndRouteBindingSource : BindingSource
    {

        #region - - - - - - Fields - - - - - -

        public static readonly BindingSource BodyAndRoute = new BodyAndRouteBindingSource("BodyAndRoute", "BodyAndRoute", true, true);

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        private BodyAndRouteBindingSource(string id, string displayName, bool isGreedy, bool isFromRequest) : base(id, displayName, isGreedy, isFromRequest)
        {
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override bool CanAcceptDataFrom(BindingSource bindingSource)
            => bindingSource == Body || bindingSource == this;

        #endregion Methods

    }

}
