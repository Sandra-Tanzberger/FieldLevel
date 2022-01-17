using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Threading;

namespace FieldLevel.Models
{
    public class RequestCachingBackgroundService: BackgroundService
    {
        private readonly ILogger _logger;
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("?Request Caching Service started...");

            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    //getposts and cache


                    await Task.Delay(60000);
                }
            }
            catch (TaskCanceledException ex)
            {
                _logger.LogWarning(ex.ToString());
            }
            catch (System.Exception ex)
            {
                _logger.LogError(ex.ToString());
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Request Caching Service stopped.");
            await base.StopAsync(cancellationToken);
        }

    }
}
