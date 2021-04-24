using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using Domain;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApi.Modules;
using Microsoft.OpenApi.Models;
using Application.UseCases.AddJobsList;

namespace WebApi
{
    /// <summary>
    /// ����� ��� ������� ����������
    /// </summary>
    public class Startup
    {
        #region ��������

        /// <summary>
        /// ������������
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region �����������

        /// <summary>
        /// ������
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region ������

        /// <summary>
        /// ����������� ����������� ��������
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSQLServer(this.Configuration);
            services.AddControllers();

           // services.AddHttpContextAccessor();

            services.AddScoped<IAddJobsListUseCase, AddJobsListUseCase>();

            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Jobs API",
                    Description = "ASP.NET Core Web API",
                });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// ������������
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
        
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion
    }
}
