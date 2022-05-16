using BookLibrary.Core.Domain;
using BookLibrary.Core.Repositories;
using BookLibrary.Infrastructure.AbstractServices;
using BookLibrary.Infrastructure.Repositories;
using BookLibrary.Infrastructure.Services;
using BookLibrary.WebAPI.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookLibrary.WebAPI
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
                                  builder =>
                                  {
                                      builder.WithOrigins("http://localhost:3000")
                                      .AllowAnyMethod()
                                      .AllowAnyHeader();
                                  });
            });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookLibrary.WebAPI", Version = "v1" });
            });

            services.AddScoped<IRepository<Author>, Repository<Author>>();
            services.AddScoped<IAuthorService, AuthorService>();

            services.AddScoped<IRepository<Book>, Repository<Book>>();
            services.AddScoped<IBookService, BookService>();

            services.AddScoped<IRepository<BookSeries>, Repository<BookSeries>>();
            services.AddScoped<IBookSeriesService, BookSeriesService>();

            services.AddScoped<IRepository<Category>, Repository<Category>>();
            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IRepository<Library>, Repository<Library>>();
            services.AddScoped<ILibraryService, LibraryService>();

            services.AddScoped<IRepository<Publisher>, Repository<Publisher>>();
            services.AddScoped<IPublisherService, PublisherService>();

            services.AddScoped<IRepository<User>, Repository<User>>();
            services.AddScoped<IUserService, UserService>();

            services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DbConnectionString"))
            );

            services.AddTransient<ExceptionHandlingMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookLibrary.WebAPI v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
