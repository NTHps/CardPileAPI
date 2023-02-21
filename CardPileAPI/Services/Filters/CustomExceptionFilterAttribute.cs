using AutoMapper;
using CardPile.Application.Errors;
using CardPile.Application.Exceptions;
using CardPile.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CardPileAPI.Services.Filters
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        #region - - - - - - Fields - - - - - -

        const string CONTENT_TYPE_APPLICATION_JSON = "application/json";

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomExceptionFilterAttribute(IMapper mapper)
        {
            this.m_Mapper = mapper;
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override void OnException(ExceptionContext context) => this.OnException(context.Exception, context);


        // Private Methods ---------------------------------------------------------------

        private static async Task<string> GetRequestBody(ExceptionContext context)
        {
            _ = context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            using var _StreamReader = new StreamReader(context.HttpContext.Request.Body);
            var _String = await _StreamReader.ReadToEndAsync();
            return _String;
        }

        private void OnException(Exception exception, ExceptionContext context)
        {
            switch (exception)
            {
                case AggregateException agge: this.HandleAggregateException(agge, context); return;
                case ArgumentNullException ane: this.HandleArgumentNullException(ane, context); return;
                case AutoMapperMappingException amme: this.HandleAutoMapperMappingException(amme, context); return;
                case HttpRequestException hr: this.HandleHttpRequestException(hr, context); return;
                case InvalidCastException ic: this.HandleInvalidCastException(ic, context); return;
                case InvalidEnumException iee: this.HandleInvalidEnumException(iee, context); return;
                case OAuthException oae: this.HandleOAuthError(oae, context); return;
                default: this.HandleUncaughtException(context); return;
            }
        }

        private static void SetResponseAsJsonContentReturn(object error, HttpStatusCode httpStatusCode, ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = CONTENT_TYPE_APPLICATION_JSON;
            context.HttpContext.Response.StatusCode = (int)httpStatusCode;
            context.Result = new JsonResult(error);
        }

        #endregion Methods

        #region - - - - - - Exception Handlers - - - - - -

        private void HandleAggregateException(AggregateException agge, ExceptionContext context) => this.OnException(agge.InnerException, context);

        private void HandleArgumentNullException(ArgumentNullException argumentNullException, ExceptionContext context)
        {
            var _ApplicationError = this.m_Mapper.Map<ValidationProblemDetails>(argumentNullException);

            SetResponseAsJsonContentReturn(_ApplicationError, HttpStatusCode.BadRequest, context);
        }

        private void HandleAutoMapperMappingException(AutoMapperMappingException amme, ExceptionContext context)
            => this.OnException(amme.InnerException, context);

        private void HandleHttpRequestException(HttpRequestException httpRequestException, ExceptionContext context)
        {
            var _HttpError = this.m_Mapper.Map<ValidationProblemDetails>(httpRequestException);
            SetResponseAsJsonContentReturn(_HttpError, HttpStatusCode.BadRequest, context);
        }

        private void HandleInvalidCastException(InvalidCastException invalidCastException, ExceptionContext context)
        {
            var _InvalidCastException = this.m_Mapper.Map<ValidationProblemDetails>(invalidCastException);

            SetResponseAsJsonContentReturn(_InvalidCastException, HttpStatusCode.BadRequest, context);
        }

        private void HandleInvalidEnumException(InvalidEnumException invalidEnumExceptionException, ExceptionContext context)
            => SetResponseAsJsonContentReturn(this.m_Mapper.Map<ValidationProblemDetails>(invalidEnumExceptionException), HttpStatusCode.BadRequest, context);

        private void HandleOAuthError(OAuthException oAuthException, ExceptionContext context)
        {
            var _OAuthError = this.m_Mapper.Map<OAuthError>(oAuthException);

            if (!string.IsNullOrEmpty(oAuthException.Error) && Enum.TryParse(typeof(OAuthErrorValuesEnum), oAuthException.Error, out var _ErrorValue) && (OAuthErrorValuesEnum)_ErrorValue == OAuthErrorValuesEnum.invalid_client)
            {
                SetResponseAsJsonContentReturn(_OAuthError, HttpStatusCode.Unauthorized, context);
                return;
            }

            SetResponseAsJsonContentReturn(_OAuthError, HttpStatusCode.BadRequest, context);
        }

        private void HandleUncaughtException(ExceptionContext context)
        {
            var _Error = new
            {
                Error = new[] { context.Exception.Message },
                StackTrace = context.Exception.StackTrace
            };

            SetResponseAsJsonContentReturn(_Error, HttpStatusCode.InternalServerError, context);
        }

        #endregion Exception Handlers

    }

}
