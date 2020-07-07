using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using StudentManagement.Models;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace StudentManagement
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false).AddXmlSerializerFormatters();// only add core service of MVC => services.AddMvcCore();
            services.AddDbContextPool<AppDbContext>(options => options.UseMySql(_configuration.GetConnectionString("studentDBconnection"), mySqlOptions => mySqlOptions
                     .ServerVersion(new Version(5, 7, 29), ServerType.MySql)));

            //services.AddMvc().AddXmlSerializerFormatters();
            //services.AddSingleton<IStudentRepository,MockStudentsRepository>();
            //services.AddScoped<IStudentRepository, MockStudentsRepository>();
            services.AddScoped<IStudentRepository, studentRepository>();

           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseMvc(routes => routes.MapRoute("default","{controller=Home}/{action=Index}/{id?}"));
          

        }
    }
}
