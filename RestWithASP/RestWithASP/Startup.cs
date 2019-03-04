using System;
using System.Collections.Generic;

using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;

using RestWithASP.Model.Context;
using RestWithASP.Business;
using RestWithASP.Business.Implementation;
using RestWithASP.Repository.Generic;

namespace RestWithASP
{
    public class Startup
    {
        private readonly ILogger _logger;
        public IConfiguration _configuration { get; }
        public IHostingEnvironment _enviroment { get; }
        public Startup(IConfiguration configuration, IHostingEnvironment environment, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _enviroment = environment;
            _logger = logger;
        }
       
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = _configuration["MySqlConnection:MySqlConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connectionString));

            if (_enviroment.IsDevelopment())
            {
                try
                {
                    var evolveConnection = new MySql.Data.MySqlClient.MySqlConnection(connectionString);

                    var evolve = new Evolve.Evolve("evolve.json", evolveConnection, msg => _logger.LogInformation(msg))
                    {
                        Locations = new List<string> { "db/migrations" },
                        IsEraseDisabled = true,
                    };

                    evolve.Migrate();

                }
                catch (Exception ex)
                {

                    _logger.LogCritical("Database migration faleid", ex);
                    throw;
                }
            }

            services.AddMvc();

            services.AddApiVersioning(option => option.ReportApiVersions = true);

            //Dependency Injection
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            //services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();

            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                loggerFactory.AddConsole(_configuration.GetSection("Logging"));
                loggerFactory.AddDebug();

                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
