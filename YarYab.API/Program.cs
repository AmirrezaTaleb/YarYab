using NLog;
using NLog.Config;
using NLog.Targets;
using NLog.Web;
using NLog.Targets.Wrappers;

namespace YarYab.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region NLog
            var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            #endregion

            try
            {
                logger.Debug("init main");
                await CreateHostBuilder(args).Build().RunAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Flush();
                LogManager.Shutdown();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNLog()
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseNLog();
                    webBuilder.UseStartup<Startup>();
                });


    }
}


