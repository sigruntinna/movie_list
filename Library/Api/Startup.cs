using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Repositories;
using Repositories.DataAccess;
using Services;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc.Cors.Internal;

namespace Api
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
            services.AddCors(options => {
                options.AddPolicy("Cors",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
            services.AddMvc();
            services.AddTransient<IUsersRespository, UsersRepository>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IBooksRespository, BooksRepository>();
            services.AddTransient<IBooksService, BooksService>();
            services.AddTransient<IReviewsRepository, ReviewsRepository>();
            services.AddTransient<IReviewsService, ReviewsService>();
            services.AddDbContext<AppDataContext>(
            options => options.UseSqlite("Data Source=../Repositories/bokasafn.db", b => b.MigrationsAssembly("Api")));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                new Info
                {
                    Title = "Johanna's Library API",
                    Version = "v1",
                    Description = "Jóhanna's (@Efri-Brú) Library system for users to loan books, manage their loans and get book recommendations.",
                    TermsOfService = "None",
                    Contact = new Contact {
                        Name = "Birkir Brynjarsson, Jóhanna Svövudóttir, Sigrún Tinna Gissurardóttir & Unnur Sól Ingimarsdóttir",
                        Email ="johannas15@ru.is",
                        Url =""
                    }
                });
                var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "SWCapi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("Cors");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });
        }
    }
}
