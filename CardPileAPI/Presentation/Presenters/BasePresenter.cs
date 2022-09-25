using AutoMapper;
using CardPile.Application.Infrastructure.Authorisation;
using CardPile.Application.Infrastructure.Validation;
using CleanArchitecture.Mediator;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CardPileAPI.Presentation.Presenters
{

    public abstract class BasePresenter :
        IAuthenticationOutputPort,
        IAuthorisationOutputPort<AuthorisationResult>,
        IValidationOutputPort<CleanValidationResult>,
        IBusinessRuleValidationOutputPort<CleanValidationResult>
    {

        #region - - - - - - Fields - - - - - -

        private readonly IMapper m_Mapper;

        #endregion Fields

        #region - - - - - - Constructors - - - - - -

        protected BasePresenter(IMapper mapper)
            => this.m_Mapper = mapper ?? throw new System.ArgumentNullException(nameof(mapper));

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public bool PresentedSuccessfully { get; protected set; }

        public IActionResult Result { get; private set; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        protected Task BadRequestAsync(ValidationProblemDetails validationProblemDetails)
        {
            this.Result = new BadRequestObjectResult(validationProblemDetails);
            return Task.CompletedTask;
        }

        protected Task CreatedAsync<TResult>(Func<TResult> mapFunc)
        {
            this.Result = new LateCreatedResult<TResult>(string.Empty, mapFunc);
            return Task.CompletedTask;
        }

        protected Task ForbiddenAsync(ProblemDetails problemDetails)
        {
            this.Result = new UnauthorizedObjectResult(problemDetails);
            return Task.CompletedTask;
        }

        protected Task NotFoundAsync(string EntityName, long entityID)
            => this.BadRequestAsync(new ValidationProblemDetails()
            {
                Detail = $"'{EntityName}' with the ID '{entityID}' was not found.",
                Status = (int)HttpStatusCode.BadRequest,
                Title = "Entity was not found.",
                Type = "https://httpstatuses.com/400"
            });

        protected Task NotFoundRouteAsync(string EntityName, long entityID)
        {
            this.Result = new NotFoundObjectResult(new ProblemDetails()
            {
                Detail = $"'{EntityName}' with the ID '{entityID}' was not found.",
                Status = (int)HttpStatusCode.NotFound,
                Title = "Entity was not found.",
                Type = "https://httpstatuses.com/400"
            });
            return Task.CompletedTask;
        }

        protected Task OkAsync<TResult>(TResult result)
        {
            this.Result = new OkObjectResult(result);
            return Task.CompletedTask;
        }

        public Task PresentBusinessRuleValidationFailureAsync(CleanValidationResult validationFailure, CancellationToken cancellationToken)
            => this.BadRequestAsync(this.m_Mapper.Map<ValidationProblemDetails>(validationFailure));

        public Task PresentUnauthenticatedAsync(CancellationToken cancellationToken)
            => this.UnauthorisedAsync();

        public Task PresentUnauthorisedAsync(AuthorisationResult authorisationFailure, CancellationToken cancellationToken)
            => this.ForbiddenAsync(this.m_Mapper.Map<ProblemDetails>(authorisationFailure));

        public Task PresentValidationFailureAsync(CleanValidationResult validationFailure, CancellationToken cancellationToken)
            => this.BadRequestAsync(this.m_Mapper.Map<ValidationProblemDetails>(validationFailure));

        protected Task UnauthorisedAsync()
        {
            this.Result = new UnauthorizedResult();
            return Task.CompletedTask;
        }

        #endregion Methods

        #region - - - - - - Nested Classes - - - - - -

        private class LateCreatedResult<TResult> : CreatedResult
        {

            #region - - - - - - Constructors - - - - - -

            public LateCreatedResult(string location, Func<TResult> resultFunc) : base(location, resultFunc) { }

            #endregion Constructors

            #region - - - - - - Methods - - - - - -

            public override Task ExecuteResultAsync(ActionContext context)
            {
                this.Value = ((Func<TResult>)this.Value).Invoke();

                return base.ExecuteResultAsync(context);
            }

            #endregion Methods

        }

        #endregion Nested Classes

    }

}
