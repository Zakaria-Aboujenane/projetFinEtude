using System;
using System.Diagnostics;
using System.Threading.Tasks;
using CalendrierDesArchives.DAO;
using Hangfire;
using Hangfire.Dashboard.Dark;
using Hangfire.SqlServer;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(CalendrierDesArchives.Startup1))]

namespace CalendrierDesArchives
{
    public class Startup1
    {
        private System.Collections.Generic.IEnumerable<IDisposable> GetHangfireServers()
        {
            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(ConnectionHelper.conVal("CalendrierDatabase"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    UsePageLocksOnDequeue = true,
                    DisableGlobalLocks = true
                }).UseDarkDashboard();

            yield return new BackgroundJobServer();
        }

        public void Configuration(IAppBuilder app)
        {
           
            app.UseHangfireAspNet(GetHangfireServers);
            app.UseHangfireDashboard();
           // app.UseHangfireDashboard();
            // Let's also create a sample background job
           // BackgroundJob.Enqueue(() => Debug.WriteLine("Hello world from Hangfire!"));

            // ...other configuration logic
        }
    }
}

