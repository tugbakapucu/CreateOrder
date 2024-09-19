using Api.Attribute;
using ApplicationService.Handler.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Container;
using Container.Modules;


namespace Api
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup()
        {
            var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            var builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{environmentName}.json", optional: false)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            RepositoryModule.AddDbContext(services, Configuration);


            services
               .AddControllers(options => options.Filters.Add(new ValidateModelAttribute(Bootstrapper.Container.Resolve<IAppLogger>())))
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
                });


            //var authenticationSchemeKey = "Bearer";
            //services.AddAuthentication(authenticationSchemeKey)
            //    .AddJwtBearer(authenticationSchemeKey, options =>
            //    {
            //        options.Authority = Configuration["Token:Endpoint"];
            //        options.RequireHttpsMetadata = true;
            //        options.TokenValidationParameters = new()
            //        {
            //            ValidateAudience = false
            //        };
            //    });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IHostApplicationLifetime applicationLifetime)
        {
            var container = app.ApplicationServices.GetAutofacRoot();
            Bootstrapper.SetContainer(container);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseExceptionHandler("/error");
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "API"); });
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            Bootstrapper.RegisterModules(builder);
        }
    }
}