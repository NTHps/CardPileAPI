using AutoMapper;
using CardPile.Application.Infrastructure.Authorisation;
using CardPile.Application.Infrastructure.Validation;
using CardPile.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardPileAPI.Infrastructure
{

    public class ErrorMappingProfile : Profile
    {

        #region - - - - - - Constructors - - - - - -

        public ErrorMappingProfile()
        {
            this.AddAuthorisationResultMapping();
            this.AddInvalidEnumExceptionMapping();
            this.AddCleanValidationResultMapping();
        }

        #endregion Constructors

        #region - - - - - - Methods - - - - - -

        private void AddAuthorisationResultMapping()
            => this.CreateMap<AuthorisationResult, ProblemDetails>()
                    .ConvertUsing((ar, pd) =>
                    {
                        var _Details = new ValidationProblemDetails
                        {
                            Detail = ar.FailureReason,
                            Status = (int)HttpStatusCode.BadRequest,
                            Title = "Authorisation Failure",
                            Type = "https://httpstatuses.com/400"
                        };

                        return _Details;
                    });

        private void AddInvalidEnumExceptionMapping()
            => this.CreateMap<InvalidEnumException, ValidationProblemDetails>()
                    .ConvertUsing((iee, vpd) =>
                    {
                        var _Details = new ValidationProblemDetails
                        {
                            Detail = iee.Message,
                            Status = (int)HttpStatusCode.BadRequest,
                            Title = iee.Message,
                            Type = "https://httpstatuses.com/400"
                        };
                        _Details.Errors.Add(string.Empty, new string[] { iee.Message });
                        return _Details;
                    });

        private void AddCleanValidationResultMapping()
            => this.CreateMap<CleanValidationResult, ValidationProblemDetails>()
                .ConvertUsing((vr, vpd) =>
                {
                    var _Details = new ValidationProblemDetails
                    {
                        Status = (int)HttpStatusCode.BadRequest,
                        Title = "Bad Request",
                        Type = "https://httpstatuses.com/400"
                    };
                    vr.Errors.ToList().ForEach(vf => _Details.Errors.Add(vf.PropertyName, new string[] { vf.ErrorMessage }));

                    return _Details;
                });

        #endregion Methods

    }

}
