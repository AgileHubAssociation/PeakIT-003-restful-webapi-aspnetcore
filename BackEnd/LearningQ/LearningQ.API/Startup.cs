using System;
using AutoMapper;
using LearningQ.DAL;
using LearningQ.DAL.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LearningQ.API
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
            services.AddDbContext<QueueDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("SQLiteConnection")));

            services.AddCors();

            services.AddControllers();

            // services.AddSingleton<IRepository, MockRepository>();
            services.AddScoped<IRepository, SQLiteRepository>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSwaggerGen();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, QueueDbContext dbContext)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // in case we switch branch in loose the DB
                // this needs to be commented when applying migrations
                dbContext.Database.EnsureCreated(); 
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            // by default it can be accessed via the / route
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaningQ V1");
                c.RoutePrefix = ""; // serve swagger from the root of the domain
            });

            app.UseRouting();

            
            app.UseCors(
                options => options.WithOrigins("http://example.com", "http://www.example.com").AllowAnyMethod()
            );

            //// global cors policy (unsecure but easy, could be used for a hackathon or smth)
            //app.UseCors(x => x
            //    .AllowAnyMethod()
            //    .AllowAnyHeader()
            //    .SetIsOriginAllowed(origin => true) // allow any origin
            //    .AllowCredentials()); // allow credentials

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
