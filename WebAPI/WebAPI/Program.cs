using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System;

namespace WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks()
                .AddCheck<RandomHealthCheck>("Ready",
        tags: new[] { "ready" })
                .AddCheck<StartupProbe>("Startup",
        tags: new[] { "startup" });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.MapControllers();
            app.MapHealthChecks("/healthz");
            app.MapHealthChecks("/health/startup", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("startup")
            });
            app.MapHealthChecks("/health/ready", new HealthCheckOptions
            {
                Predicate = healthCheck => healthCheck.Tags.Contains("ready")
            });



            app.Run();
        }
    }
}