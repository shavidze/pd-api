using Application.AbstractParser;
using Application.Factory;
using Application.ImageToMemorySaver;
using Application.PdfParser;
using Application.PitchDeckExecutors;
using Application.PitchDeckExecutors.Commands;
using Application.PitchDeckExecutors.Queries;
using Application.PitchDeckProcessor;
using Application.PPTParser;
using Application.RequestModel;
using Application.Shared;
using Infrastructure.PitchDeckAppDbContext;
using Infrastructure.Repositories.PitchDeckRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.IO;

namespace PitchDeckBack
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
            services.AddCors();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PitchDeckBack", Version = "v1" });
                c.OperationFilter<SwaggerFileOperationFilter>();
            });

            services.AddDbContext<PitchDeckDbContext>(options =>
                    options.UseNpgsql(Configuration.GetConnectionString("PitchDeckConnectionString")));

            services.AddScoped<IPitchDeckProccessor, PitchDeckProccessor>();

            services.AddScoped<IImageToMemorySaver, ImageToMemorySaver>();

            services.AddScoped<IPitchDeckRepository, PitchDeckRepository>();

            services.AddScoped<ICommandExecutor<CreatePitchDeckCommand>, CreatePitchDeckCommandExecutor>();

            services.AddScoped<IQueryExecutor<PitchDeckQuery, PitchDeckQueryResult>, PitchDeckQueryExecutor>();

            services.AddScoped<ICommandDispatcher, CommandDispatcher>();

            services.AddScoped<IQueryDispatcher, QueryDispatcher>();

            services.AddScoped<IDispatcher, Dispatcher>();

            services.AddScoped<IParserFactory, ParserFactory>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PitchDeckBack v1"));

            AutoMigration(app);

            if (!Directory.Exists("Images"))
            {
                Directory.CreateDirectory("Images");
            }

            app.UseFileServer(new FileServerOptions
            {
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "Images")),
                RequestPath = "/Images",
                EnableDefaultFiles = true
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void AutoMigration(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                using (var dbContext = serviceProvider.GetService<PitchDeckDbContext>())
                {
                    dbContext.Database.Migrate();
                }
            }
        }
    }
}
