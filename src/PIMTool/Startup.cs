using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PIMTool.Db;
using PIMTool.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PIMTool.Services.Project;
using PIMTool.Db.Repository;
using PIMTool.Services.Employee;
using PIMTool.Services.Group;
using PIMTool.Shared.Extension;

namespace PIMTool
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
            ConfigureControllers(services);
            
            ConfigureSwagger(services);
            
            ConfigureAutoMapper(services);
            
            ConfigureDbContext(services);
            
            ConfigureAppServices(services);

            ConfigureUnitOfWork(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PIMTool v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PIMTool", Version = "v1" });
            });
        }

        private void ConfigureControllers(IServiceCollection services)
        {
            services.AddControllers();
        }

        private void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(EntityBase).Assembly);
        }

        private void ConfigureDbContext(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(Configuration["ConnectionStrings:Default"]));
        }

        private void ConfigureAppServices(IServiceCollection services)
        {
            services.AddSingleton<IEntitySelectorFactory, EntitySelectorFactory>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IGroupService, GroupService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        private void ConfigureUnitOfWork(IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        }
    }
}
