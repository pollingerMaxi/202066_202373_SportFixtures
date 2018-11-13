using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SportFixtures.BusinessLogic.Implementations;
using SportFixtures.BusinessLogic.Interfaces;
using SportFixtures.Data.Access;
using SportFixtures.Data.Entities;
using SportFixtures.Data.Repository;
using SportFixtures.Portal.Profiles;

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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials());
            });
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            ResolveRepositoryDependencies(services);
            ResolveBusinessLogicDependencies(services);
            var mappingConfig = new MapperConfiguration(c => c.AddProfile(new MappingProfile()));
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddDbContext<Context>(o => o.UseSqlServer(Configuration.GetSection("ConnectionString").Value));
        }

        private void ResolveBusinessLogicDependencies(IServiceCollection services)
        {
            services.AddScoped<ISportBusinessLogic, SportBusinessLogic>();
            services.AddScoped<ITeamBusinessLogic, TeamBusinessLogic>();
            services.AddScoped<IUserBusinessLogic, UserBusinessLogic>();
            services.AddScoped<ICommentBusinessLogic, CommentBusinessLogic>();
            services.AddScoped<IEncounterBusinessLogic, EncounterBusinessLogic>();
        }

        private void ResolveRepositoryDependencies(IServiceCollection services)
        {
            services.AddScoped<IRepository<Team>, GenericRepository<Team>>();
            services.AddScoped<IRepository<Sport>, GenericRepository<Sport>>();
            services.AddScoped<IRepository<User>, GenericRepository<User>>();
            services.AddScoped<IRepository<Comment>, GenericRepository<Comment>>();
            services.AddScoped<IRepository<UsersTeams>, GenericRepository<UsersTeams>>();
            services.AddScoped<IRepository<Encounter>, GenericRepository<Encounter>>();
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
            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
