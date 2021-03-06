using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Film_Listing_API.Mapper;

using Microsoft.EntityFrameworkCore;
using Film_Listing_API.Services;
using AutoMapper;
<<<<<<< HEAD
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
=======
>>>>>>> b8842780bb82513b3c1569ac25256c20124d0abc

namespace Film_Listing_API
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


            services.AddDbContext<FilmlistingContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MoviesDbConnectionString")));

            services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
            {
                builder.WithOrigins("*")
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            //services.AddAutoMapper(typeof(Startup));
            var config = new MapperConfiguration(cfg => {
                cfg.AddProfile<DomainProfile>();
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            services.AddSingleton(Configuration)
                .AddTransient<IMoviesService, MoviesService>()
                .AddTransient<IActorsService, ActorsService>()
                .AddTransient<IProducersService, ProducersService>()
                .AddTransient<IModelFactory, ModelFactory>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Film_Listing_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Film_Listing_API v1"));
            }

            app.UseCors("MyPolicy");

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
