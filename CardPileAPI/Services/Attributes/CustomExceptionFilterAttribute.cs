using AutoMapper;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CardPile.Domain.Exceptions;

namespace CardPileAPI.Services.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CustomExceptionFilterAttribute : ExceptionFilterAttribute
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        public CustomExceptionFilterAttribute(IMapper mapper)
        {
            this.m_Mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        public override void OnException(ExceptionContext context)
            => this.OnException(context.Exception, context);

        private void OnException(Exception exception, ExceptionContext context)
        {
            switch (exception)
            {
                case AggregateException agge: this.HandleAggregateException(agge, context); return;
                case AutoMapperMappingException amme: this.HandleAutoMapperMappingException(amme, context); return;
                case InvalidEnumException iee: this.HandleInvalidEnumException(iee, context); return;
                default: this.HandleUncaughtException(context); return;
            }
        }

        private static void SetResponseAsJsonContentReturn(object error, HttpStatusCode httpStatusCode, ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)httpStatusCode;
            context.Result = new JsonResult(error);
        }

        #endregion Methods

        #region - - - - - - Exception Handlers - - - - - -

        private void HandleAggregateException(AggregateException agge, ExceptionContext context)
            => this.OnException(agge.InnerException, context);

        private void HandleAutoMapperMappingException(AutoMapperMappingException amme, ExceptionContext context)
            => this.OnException(amme.InnerException, context);

        private void HandleInvalidEnumException(InvalidEnumException invalidEnumExceptionException, ExceptionContext context)
            => SetResponseAsJsonContentReturn(this.m_Mapper.Map<ValidationProblemDetails>(invalidEnumExceptionException), HttpStatusCode.BadRequest, context);

        private void HandleUncaughtException(ExceptionContext context)
        {
            var _Error = new
            {
                Error = new[] { context.Exception.Message },
                context.Exception.StackTrace
            };

            SetResponseAsJsonContentReturn(_Error, HttpStatusCode.InternalServerError, context);
        }

        #endregion Exception Handlers

    }

}
