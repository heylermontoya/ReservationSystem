
using MediatR;
using RESERVATION_SYSTEM.Api.Filters;
using RESERVATION_SYSTEM.Infrastructure.Extensions;
using System.Reflection;
using Serilog;
using Prometheus;
using Microsoft.EntityFrameworkCore;
using RESERVATION_SYSTEM.Infrastructure.Context;

public partial class Program
{
    protected Program() { }

    private static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        ConfigurationManager config = builder.Configuration;

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });

        builder.Services.AddControllers(opts =>
        {
            opts.Filters.Add(typeof(AppExceptionFilterAttribute));
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddMediatR(
            Assembly.Load("RESERVATION_SYSTEM.Application"),
            typeof(Program).Assembly
        );
        builder.Services.AddAutoMapper(
            Assembly.Load("RESERVATION_SYSTEM.Application")
        );

        string stringConnection = config["StringConnection"];

        builder.Services.AddDbContext<PersistenceContext>(opt =>
        {
            opt.UseSqlServer(stringConnection, sqlopts =>
            {
                sqlopts.MigrationsHistoryTable("_MigrationHistory", config.GetValue<string>("SchemaName"));
            });
        });

        builder.Services
            .AddHealthChecks();


        builder.Services
            .AddLogging(loggingBuilder => loggingBuilder.AddConsole()
            .AddSerilog(dispose: true));

        builder.Services.AddHttpContextAccessor();

        builder.Services
            .AddPersistence(stringConnection)
            .AddDomainServices();

        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new() { Title = "RESERVATION_SYSTEM", Version = "version 1.0.0" });
            options.CustomSchemaIds(schema => schema.FullName);
            options.DocumentFilter<BasePathFilter>(config["BasePathFilter"]);
        });

        Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

        WebApplication app = builder.Build();
        app.UseCors("AllowAll");
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "RESERVATION_SYSTEM"));

        app.UseRouting();

        app.UseHttpMetrics().UseEndpoints(endpoints =>
        {
            endpoints.MapMetrics();
            endpoints.MapHealthChecks("/health");
        });

        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }
}
