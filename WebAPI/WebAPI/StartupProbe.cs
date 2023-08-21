using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace WebAPI
{
    public class StartupProbe : IHealthCheck
    {
        private static readonly Random _rnd = new Random();
        private readonly ILogger<StartupProbe> logger;

        public StartupProbe(ILogger<StartupProbe> logger)
        {
            this.logger = logger;
        }

        //public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        //{
        //    var result = _rnd.Next(5) == 0
        //        ? HealthCheckResult.Healthy()
        //        : HealthCheckResult.Unhealthy("Failed random");

        //    return Task.FromResult(result);
        //}

        public Task<HealthCheckResult> CheckHealthAsync(
        HealthCheckContext context,
        CancellationToken cancellationToken = default(CancellationToken))
        {
            logger.Log(LogLevel.Debug, "triggered startup probe");
            var healthCheckResultHealthy = true;
            if (healthCheckResultHealthy)
            {
                return Task.FromResult(
                    HealthCheckResult.Healthy("A healthy result."));
            }

            return Task.FromResult(
                HealthCheckResult.Unhealthy("An unhealthy result."));
        }
    }
}
