using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Server.Services.Abstract;
using Server.Services.MongoDB;
using Server.Config;

namespace Server
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
            services.AddCors(options => options.AddPolicy(name: "all", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.Configure<MongoDBSettings>(Configuration.GetSection(nameof(MongoDBSettings)));

            services.AddSingleton<IDBSettings>(sp =>
                sp.GetRequiredService<IOptions<MongoDBSettings>>().Value);

            services.AddSingleton(mc =>
                new MongoClient(mc.GetRequiredService<IOptions<MongoDBSettings>>().Value.ConnectionString));
            services.AddScoped<IDoctorsService, MongoDBDoctorsService>();
            services.AddScoped<IPatientsService, MongoDBPatientsService>();
            services.AddScoped<IMedicinesService, MongoDBMedicinesService>();
            services.AddScoped<IVisitsService, MongoDBVisitsService>();
            services.AddScoped<IDBService, MongoDBService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("all");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
