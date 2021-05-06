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
using Application.UseCases.GetJobsList;
using Application.Services;
using Application.UseCases.GetJob;
using Infrastructure;

namespace WebApi
{
    /// <summary>
    /// Класс для запуска приложения
    /// </summary>
    public class Startup
    {
        #region Свойства

        /// <summary>
        /// Конфигурация
        /// </summary>
        public IConfiguration Configuration { get; }

        #endregion

        #region Конструктор

        /// <summary>
        /// Запуск
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        #region Методы

        /// <summary>
        /// Подключение необходимых сервисов
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.DataBaseConnectionConfiguration(this.Configuration);
            services.AddControllers();

           // services.AddHttpContextAccessor();

            services.AddScoped<IGetJobsListUseCase, GetJobsListUseCase>();
            services.AddScoped<IGetJobUseCase, GetJobUseCase>();
            services.AddScoped<IVacanciesService, VacanciesService>();

            services.Configure<HhRuConfig>(Configuration.GetSection(nameof(HhRuConfig)));

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
        /// Конфигурация
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
