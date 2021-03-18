using AutoMapper;
using FluentValidation.AspNetCore;
using GMCS_RestApi.Domain.Contexts;
using GMCS_RestApi.Domain.Interfaces;
using GMCS_RestApi.Domain.Providers;
using GMCS_RestApi.Domain.Services;
using GMCS_RestAPI.Middlewares;
using GMCS_RestAPI.Validators;
using GMSC_RestAPI.Infrastructure.ClientBehaviors;
using GMSC_RestAPI.Infrastructure.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using ServiceReference;
using System;
using System.IO;
using System.Reflection;
using System.ServiceModel;

namespace GMCS_RestAPI
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
            var connection = Configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

            services.AddControllers();

            services.AddScoped<IBooksProvider, BooksProvider>();
            services.AddScoped<IBooksService, BooksService>();

            services.AddTransient<IBookStore>(serviceProvider =>
            {
                var client = new BookStoreClient(new BasicHttpBinding(),
                        new EndpointAddress(Configuration.GetConnectionString("ServiceConnection")));
                client.Endpoint.EndpointBehaviors.Add(new SoapBehavior(new ClientInspector(serviceProvider.GetRequiredService<ILoggerFactory>())));

                return client;
            });

            services.AddScoped<IBookStoreRepository, BookStoreRepository>();

            services.AddScoped<IAuthorsProvider, AuthorsProvider>();
            services.AddScoped<IAuthorsService, AuthorsService>();
            services.AddScoped<IRabbitMessagesProvider, RabbitMessagesProvider>();

            services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(new Mapping.MappingProfile())).CreateMapper());

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GMCS_RestAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });

            services.AddMvc(options => options.Filters.Add(typeof(ModelStateValidator)))
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Startup>();
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GMCS_RestAPI v1"));
            }

            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}