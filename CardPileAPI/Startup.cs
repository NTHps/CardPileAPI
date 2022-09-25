//namespace CardPileAPI
//{

//    public class Startup
//    {

//        #region - - - - - - Constructors - - - - - -

//        public Startup(IConfiguration configuration)
//            => this.Configuration = configuration;

//        #endregion Constructors

//        #region - - - - - - Properties - - - - - -

//        public IConfiguration Configuration { get; }

//        #endregion Properties

//        #region - - - - - - Methods - - - - - -

//        public void ConfigureServices(IServiceCollection services)
//        {
//            this.ConfigureAppContextSettings(services);

//            //services.AddApiControllers();
//            //services.AddAuthenticationServices();
//            //services.AddAutoMapperService();
//            //services.AddCleanArchitectureServices();
//            //services.AddCors();
//            //services.AdInterfaceAdaptersServices();
//            //services.AddPersistenceContext(this.Configuration);
//            //services.AddSwaggerServices();
//        }

//        public void Configure(IApplicationBuilder app, IPersistenceContext persistenceContext, IWebHostEnvironment env)
//        {
//            if (env.IsDevelopment())
//            {
//                app.UseSwagger();
//                app.UseSwaggerUI(c =>
//                {
//                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Catering Pro V1");
//                    c.RoutePrefix = string.Empty;
//                });

//                app.UseDeveloperExceptionPage();
//            }

//            app.UseHttpsRedirection();
//            app.UseRouting();
//            app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
//            app.UseAuthentication();
//            app.UseAuthorization();
//            app.UseEndpoints(endpoints =>
//            {
//                endpoints.MapControllers();
//            });

//            var _PersistenceContext = (PersistenceContext)persistenceContext;
//            _PersistenceContext.Database.Migrate();
//        }

//        #endregion Methods

//        #region - - - - - - Service Registration - - - - - -

//        private void ConfigureAppContextSettings(IServiceCollection services)
//            => services.Configure<DataStorageOptions>(this.Configuration.GetSection("DataStorageSettings"));

//        #endregion Service Registration

//    }



//}
