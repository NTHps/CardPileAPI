using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CardPileAPI.Services.Swagger
{

    public class RequestBodyFilter : IOperationFilter
    {

        #region - - - - - - IOperationFilter Implementation - - - - - -

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var _ApiBodyParameter = context.ApiDescription.ParameterDescriptions.FirstOrDefault(p => p.Source.CanAcceptDataFrom(BindingSource.Body));
            if (_ApiBodyParameter == null) return;

            var _SwaggerQueryParameter = operation.Parameters.FirstOrDefault(p => p.Name == _ApiBodyParameter.Name && p.In == ParameterLocation.Query);
            if (_SwaggerQueryParameter == null) return;

            operation.Parameters.Remove(_SwaggerQueryParameter);
            operation.RequestBody = new OpenApiRequestBody { Content = { ["application/json"] = new OpenApiMediaType { Schema = _SwaggerQueryParameter.Schema } } };
        }

        #endregion IOperationFilter Implementation

    }

}
