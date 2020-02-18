using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Newtonsoft.Json.Serialization;
using Bravent.MultiDatabaseAPI.Domain.Domains;
using Bravent.MultiDatabaseAPI.Domain.Services;
using Bravent.MultiDatabaseAPI.Infrastructure.Shared.Interfaces;

namespace Bravent.MultiDatabaseAPI
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
            services.AddControllers();

            ConfigureBasicSettings(services);

            //ConfigureSQLServer(services);
            ConfigureMongoDB(services);
            ConfigureDomains(services);

            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAuthorService, AuthorService>();
        }

        private static void ConfigureBasicSettings(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                                      .AllowAnyMethod()
                                      .AllowAnyHeader());
            });

            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
            }).SetCompatibilityVersion(CompatibilityVersion.Latest).AddApiExplorer();

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
            });

            services.AddOptions();
        }

        private void ConfigureDomains(IServiceCollection services)
        {
            services.AddScoped<IBookDomain, BookDomain>();
            services.AddScoped<IAuthorDomain, AuthorDomain>();
        }

        private void ConfigureMongoDB(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Infrastructure.Persistence.MongoDB.Helpers.Mappers());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            services.Configure<Infrastructure.Persistence.MongoDB.Implementation.MongoDBSettings>(options =>
            {
                options.CollectionName = Configuration.GetSection("MongoConnection:CollectionName").Value;
                options.Database = Configuration.GetSection("MongoConnection:Database").Value;
            });

            services.AddSingleton<IMongoClient>(new MongoClient(Configuration.GetSection("MongoConnection:ConnectionString").Value));

            services.AddScoped<IDbContext<Infrastructure.Persistence.MongoDB.DAO.Book>, Infrastructure.Persistence.MongoDB.Implementation.GenericDbContext<Infrastructure.Persistence.MongoDB.DAO.Book>>();
            services.AddScoped<IPersistence<Infrastructure.Persistence.MongoDB.DAO.Book, string>, Infrastructure.Persistence.MongoDB.Implementation.Persistence<Infrastructure.Persistence.MongoDB.DAO.Book, string>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.MongoDB.DAO.Book, string>, Infrastructure.Persistence.MongoDB.Implementation.Query<Infrastructure.Persistence.MongoDB.DAO.Book, string>>();
            services.AddScoped<Domain.Shared.Repositories.IBookRepository, Infrastructure.Persistence.MongoDB.Repositories.BookRepository>();

            services.AddScoped<IDbContext<Infrastructure.Persistence.MongoDB.DAO.Author>, Infrastructure.Persistence.MongoDB.Implementation.GenericDbContext<Infrastructure.Persistence.MongoDB.DAO.Author>>();
            services.AddScoped<IPersistence<Infrastructure.Persistence.MongoDB.DAO.Author, string>, Infrastructure.Persistence.MongoDB.Implementation.Persistence<Infrastructure.Persistence.MongoDB.DAO.Author, string>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.MongoDB.DAO.Author, string>, Infrastructure.Persistence.MongoDB.Implementation.Query<Infrastructure.Persistence.MongoDB.DAO.Author, string>>();
            services.AddScoped<Domain.Shared.Repositories.IAuthorRepository, Infrastructure.Persistence.MongoDB.Repositories.AuthorRepository>();


        }

        private void ConfigureSQLServer(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Infrastructure.Persistence.SQLServer.Helpers.Mappers());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            //services.AddDbContext<IDbContext, Infrastructure.Persistence.SQLServer.Contexts.GenericDbContext<Infrastructure.Persistence.SQLServer.DAO.Book>>(options => options.UseInMemoryDatabase("Book"), ServiceLifetime.Transient);
            services.AddDbContext<IDbContext<Infrastructure.Persistence.SQLServer.DAO.Book>, Infrastructure.Persistence.SQLServer.Implementation.GenericDbContext<Infrastructure.Persistence.SQLServer.DAO.Book>>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddScoped<IPersistence<Infrastructure.Persistence.SQLServer.DAO.Book, Guid?>, Infrastructure.Persistence.SQLServer.Implementation.Persistence<Infrastructure.Persistence.SQLServer.DAO.Book, Guid?>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.SQLServer.DAO.Book, Guid?>, Infrastructure.Persistence.SQLServer.Implementation.Query<Infrastructure.Persistence.SQLServer.DAO.Book, Guid?>>();
            services.AddScoped<Domain.Shared.Repositories.IBookRepository, Infrastructure.Persistence.SQLServer.Repositories.BookRepository>();

            services.AddDbContext<IDbContext<Infrastructure.Persistence.SQLServer.DAO.Author>, Infrastructure.Persistence.SQLServer.Implementation.GenericDbContext<Infrastructure.Persistence.SQLServer.DAO.Author>>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
            services.AddScoped<IPersistence<Infrastructure.Persistence.SQLServer.DAO.Author, Guid?>, Infrastructure.Persistence.SQLServer.Implementation.Persistence<Infrastructure.Persistence.SQLServer.DAO.Author, Guid?>>();
            services.AddScoped<IQuery<Infrastructure.Persistence.SQLServer.DAO.Author, Guid?>, Infrastructure.Persistence.SQLServer.Implementation.Query<Infrastructure.Persistence.SQLServer.DAO.Author, Guid?>>();
            services.AddScoped<Domain.Shared.Repositories.IAuthorRepository, Infrastructure.Persistence.SQLServer.Repositories.AuthorRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseMvc();

            app.UseAuthorization();
        }
    }
}
