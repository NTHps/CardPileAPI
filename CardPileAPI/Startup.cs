using CardPile.Application.Infrastructure.Authorisation;
using CardPile.Application.Infrastructure.Validation;
using CardPile.Application.Services.Persistence;
using CardPile.Persistence.Persistence;
using CardPileAPI.Infrastructure.Authentication;
using CardPileAPI.Infrastructure.Configuration;
using CardPileAPI.Infrastructure.ModelBinding;
using CardPileAPI.Services.Swagger;
using CleanArchitecture.Mediator.Authentication;
using CleanArchitecture.Mediator.DependencyInjection;
using CleanArchitecture.Mediator.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Text.Json.Serialization;

namespace CardPile.WebApi
{

    public class Startup
    {

        #region - - - - - - Constructors - - - - - -

        public Startup(IConfiguration configuration)
            => this.Configuration = configuration;

        #endregion Constructors

        #region - - - - - - Properties - - - - - -

        public IConfiguration Configuration { get; }

        #endregion Properties

        #region - - - - - - Methods - - - - - -

        public void ConfigureServices(IServiceCollection services)
        {
            this.ConfigureAppContextSettings(services);

            services.AddApiControllers();
            services.AddAuthenticationServices();
            services.AddAutoMapperService();
            services.AddCleanArchitectureServices();
            services.AddCors();
            services.AdInterfaceAdaptersServices();
            services.AddPersistenceContext(this.Configuration);
            services.AddSwaggerServices();
        }

        public void Configure(IApplicationBuilder app, IPersistenceContext persistenceContext, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();

                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var _PersistenceContext = (PersistenceContext)persistenceContext;
            _PersistenceContext.Database.Migrate();
        }

        #endregion Methods

        #region - - - - - - Service Registration - - - - - -

        private void ConfigureAppContextSettings(IServiceCollection services)
            => services.Configure<DataStorageOptions>(this.Configuration.GetSection("DataStorageSettings"));

        #endregion Service Registration

    }

    internal static class IServiceCollectionExtensions
    {

        #region - - - - - - Methods - - - - - -

        public static void AddApiControllers(this IServiceCollection services)
            => services.AddControllers(options =>
            {
                options.ModelBinderProviders.Insert(0,
                    new BodyAndRouteModelBinderProvider(
                        options.ModelBinderProviders.OfType<BodyModelBinderProvider>().Single(),
                        options.ModelBinderProviders.OfType<ComplexObjectModelBinderProvider>().Single())
                        );
            }
            ).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));

        public static void AddAuthenticationServices(this IServiceCollection services)
        {
            services.AddScoped<IAuthenticatedClaimsPrincipalProvider, AuthenticatedClaimsPrincipalProvider>();
            services.AddHttpContextAccessor();
        }

        public static void AddAutoMapperService(this IServiceCollection services)
            => services.AddAutoMapper(GetAssemblies());

        public static void AddCleanArchitectureServices(this IServiceCollection services)
        {
            CleanArchitectureMediator.Register(opts =>
                  _ = opts.ConfigurePipeline(pipeline =>
                              pipeline.AddAuthentication()
                                  .AddAuthorisation<AuthorisationResult>()
                                  .AddBusinessRuleValidation<CleanValidationResult>()
                                  .AddInputPortValidation<CleanValidationResult>()
                                  .AddInteractorInvocation())
                          .ScanAssemblies(Assembly.GetExecutingAssembly(), GetAssemblies())
                          .SetRegistrationAction((serviceType, implementationType) =>
                              _ = services.AddScoped(serviceType, implementationType))
                          .Validate());

            services.AddScoped<UseCaseServiceResolver>(serviceProvider => serviceProvider.GetService);
        }

        public static void AdInterfaceAdaptersServices(this IServiceCollection services)
            => _ = services.Scan(s => s.FromAssemblies(InterfaceAdapters.AssemblyUtility.GetAssembly())
                                        .AddClasses(c => c.Where(type => type.Name.EndsWith("Controller")))
                                        .AsSelf()
                                        .WithScopedLifetime());

        public static void AddPersistenceContext(this IServiceCollection services, IConfiguration configuration)
            => services.AddDbContext<IPersistenceContext, PersistenceContext>(options =>
            {
                var _DataStorageOptions = configuration.GetSection("DataStorageSettings").Get<DataStorageOptions>();

                options.UseSqlServer(_DataStorageOptions.DatabaseConnectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
                    .EnableSensitiveDataLogging(sensitiveDataLoggingEnabled: true);
            });

        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<RequestBodyFilter>();
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Card Pile", Version = "v1" });
            });
        }

        private static Assembly[] GetAssemblies()
            => new[] { Assembly.GetExecutingAssembly(), Application.Infrastructure.AssemblyUtility.GetAssembly(), Persistence.AssemblyUtility.GetAssembly() };

        #endregion Methods

    }

}
