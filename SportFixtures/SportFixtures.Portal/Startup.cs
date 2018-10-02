using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;

namespace SportFixtures.Portal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            ResolveRepositoryDependencies(services);
            ResolveBusinessLogicDependencies(services);
            services.AddDbContext<Context>(o => o.UseSqlServer(@"Server=.;Database=SportFixturesTest;Trusted_Connection=True;Integrated Security=True"));
        }

        private void ResolveBusinessLogicDependencies(IServiceCollection services)
        {
            services.AddScoped<ISportBusinessLogic, SportBusinessLogic>();
            services.AddScoped<ITeamBusinessLogic, TeamBusinessLogic>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
        }

        private void ResolveRepositoryDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepository<Team>, GenericRepository<Team>>();
            services.AddScoped<IRepository<Sport>, GenericRepository<Sport>>();
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
