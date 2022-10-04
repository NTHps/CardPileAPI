using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

namespace CardPileAPI.Infrastructure.ModelBinding
{

    public class BodyAndRouteModelBinderProvider : IModelBinderProvider
    {

        #region - - - - - - Fields - - - - - -

        private BodyModelBinderProvider m_BodyModelBinderProvider;
        private ComplexObjectModelBinderProvider m_ComplexObjectModelBinderProvider;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public BodyAndRouteModelBinderProvider(BodyModelBinderProvider bodyModelBinderProvider, ComplexObjectModelBinderProvider complexObjectModelBinderProvider)
        {
            this.m_BodyModelBinderProvider = bodyModelBinderProvider ?? throw new ArgumentNullException(nameof(bodyModelBinderProvider));
            this.m_ComplexObjectModelBinderProvider = complexObjectModelBinderProvider ?? throw new ArgumentNullException(nameof(complexObjectModelBinderProvider));
        }

        #endregion Constructors

        #region - - - - - - IModelBinderProvider Implementation - - - - - -

        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context?.BindingInfo?.BindingSource?.CanAcceptDataFrom(BodyAndRouteBindingSource.BodyAndRoute) ?? false)
                return new BodyAndRouteModelBinder(this.m_BodyModelBinderProvider.GetBinder(context), this.m_ComplexObjectModelBinderProvider.GetBinder(context));

            return null;
        }

        #endregion IModelBinderProvider Implementation

    }

}
