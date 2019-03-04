using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using SampleProject.API.Modules;
using SampleProject.API.SeedWork;
using SampleProject.Domain.SeedWork;
using SampleProject.Infrastructure;

[assembly: UserSecretsId("54e8eb06-aaa1-4fff-9f05-3ced1cb623c2")]
namespace SampleProject.API
{  
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private const string OrdersConnectionString = "OrdersConnectionString";

        public Startup(IHostingEnvironment env)
        {
            this._configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddJsonFile($"hosting.{env.EnvironmentName}.json")
                .AddUserSecrets<Startup>()
                .Build();
        }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services
                .AddEntityFrameworkSqlServer()
                .AddDbContext<OrdersContext>(options =>
                {
                    options.UseSqlServer(this._configuration[OrdersConnectionString]);
                });

            services.AddProblemDetails(x =>
            {
                x.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));
                x.Map<BusinessRuleValidationException>(ex => new BusinessRuleValidationExceptionProblemDetails(ex));
            });

            return CreateAutofacServiceProvider(services);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseProblemDetails();
            }

            app.UseMvc();
        }

        private IServiceProvider CreateAutofacServiceProvider(IServiceCollection services)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new InfrastructureModule(this._configuration[OrdersConnectionString]));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ForeignExchangeModule());
            container.RegisterModule(new DomainModule());

            var children = this._configuration.GetSection("Caching").GetChildren();
            Dictionary<string, TimeSpan> configuration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            container.RegisterModule(new CachingModule(configuration));

            return new AutofacServiceProvider(container.Build());
        }
    }
}
