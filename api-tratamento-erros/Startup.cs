using api_tratamento_erros.ConnectDB;
using api_tratamento_erros.Errors;
using api_tratamento_erros.Models;
using api_tratamento_erros.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_tratamento_erros
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
            var connectionMysql = Configuration.GetConnectionString("mysql");
            services.AddDbContext<DBContext>(option => option.UseMySql(
                connectionMysql,
                ServerVersion.AutoDetect(connectionMysql)
                ));

            services.AddControllers(
                options => {
                    options.SuppressAsyncSuffixInActionNames = false;
                }
                );
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(
                options =>
                {
                    options.SwaggerDoc("v1",
                        new Microsoft.OpenApi.Models.OpenApiInfo
                        {
                            Title = "Escola Estadual",
                            Version = "v1",
                            Description = "Projeto de demonstração ASP.Net Core",
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Name = "Ans Dev",
                                // Url = ""
                            }
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(
                    options =>
                    {
                        options.RoutePrefix = "swagger";
                        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                    });
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseCustomExceptionMiddleware();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
