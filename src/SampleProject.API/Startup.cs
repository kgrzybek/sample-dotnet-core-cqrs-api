using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Builder;
using Autofac.Extensions.DependencyInjection;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.UserSecrets;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SampleProject.API.InternalCommands;
using SampleProject.API.Modules;
using SampleProject.API.Outbox;
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

        private ISchedulerFactory _schedulerFactory;
        private IScheduler _scheduler;

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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
            IApplicationLifetime lifetime, IServiceProvider serviceProvider)
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

            this.StartQuartz(serviceProvider);
        }

        private IServiceProvider CreateAutofacServiceProvider(IServiceCollection services)
        {
            var container = new ContainerBuilder();

            container.Populate(services);

            container.RegisterModule(new InfrastructureModule(this._configuration[OrdersConnectionString]));
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ForeignExchangeModule());
            container.RegisterModule(new DomainModule());
            container.RegisterModule(new EmailModule());

            var children = this._configuration.GetSection("Caching").GetChildren();
            Dictionary<string, TimeSpan> configuration = children.ToDictionary(child => child.Key, child => TimeSpan.Parse(child.Value));
            container.RegisterModule(new CachingModule(configuration));

            return new AutofacServiceProvider(container.Build());
        }

        public void StartQuartz(IServiceProvider serviceProvider)
        {
            this._schedulerFactory = new StdSchedulerFactory();
            this._scheduler = _schedulerFactory.GetScheduler().GetAwaiter().GetResult();

            var container = new ContainerBuilder();
            container.RegisterModule(new OutboxModule());
            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new InfrastructureModule(this._configuration[OrdersConnectionString]));
            container.RegisterModule(new EmailModule());

            container.Register(c =>
            {
                var dbContextOptionsBuilder = new DbContextOptionsBuilder<OrdersContext>();
                dbContextOptionsBuilder.UseSqlServer(this._configuration[OrdersConnectionString]);

                return new OrdersContext(dbContextOptionsBuilder.Options);
            }).AsSelf().InstancePerLifetimeScope();

            _scheduler.JobFactory = new JobFactory(container.Build());

            _scheduler.Start().GetAwaiter().GetResult();

            var processOutboxJob = JobBuilder.Create<ProcessOutboxJob>().Build();
            var trigger = 
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();

            _scheduler.ScheduleJob(processOutboxJob, trigger).GetAwaiter().GetResult(); 

            var processInternalCommandsJob = JobBuilder.Create<ProcessInternalCommandsJob>().Build();
            var triggerCommandsProcessing = 
                TriggerBuilder
                    .Create()
                    .StartNow()
                    .WithCronSchedule("0/15 * * ? * *")
                    .Build();
            _scheduler.ScheduleJob(processInternalCommandsJob, triggerCommandsProcessing).GetAwaiter().GetResult();           
        }
    }
}
