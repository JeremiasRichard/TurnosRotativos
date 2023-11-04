using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using TurnosRotativos.Database;
using TurnosRotativos.Exceptions;
using TurnosRotativos.Interfaces;
using TurnosRotativos.Repositories;
using TurnosRotativos.Services;

namespace TurnosRotativos
{
    public class Startup
    {

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(x =>
            x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen();


            /*Dependencia de la DB*/
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            var port = Configuration.GetSection("Server:Port").Value;

            /*Configuracion de CORS*/
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("https://neorislab.stoplight.io").AllowAnyMethod().AllowAnyHeader()
                    .WithExposedHeaders(new string[] { "TotalRegisters", "Records", "RecorsPerPage" });

                });
            });


            /*Dependencias de repositorios y servicios*/
            services.AddTransient<IEmpleadoRepository, EmpleadoRepositoryImp>();
            services.AddScoped<IEmpleadoService,EmpleadoServiceImp>();

            services.AddTransient<IConceptosRepository,ConceptoRepositoryImp>();
            services.AddScoped<IConceptosService,ConceptosServiceImp>();  

            services.AddTransient<IJornadaRepository,JornadaRepositoryImp>();
            services.AddScoped<IJornadaService, JornadaServiceImp>();
            
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseRouting();
            app.UseCors();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseMiddleware<ExceptionMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
