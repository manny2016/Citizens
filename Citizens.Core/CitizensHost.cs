

namespace Citizens.Core
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging.Console;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Data.SqlClient;
    using System.Data;
    using System.Collections.Generic;
    public static class CitizensHost
    {
        private static ServiceCollection collection;
        private static ServiceProvider provider;

        public static void ConfigureServiceProvider(Action<ServiceCollection> configure)
        {
            if (provider == null)
            {

                collection = new ServiceCollection();
                if (configure != null)
                {
                    configure(collection);
                }
                collection.AddLogging((cfg) =>
                {
                    cfg.AddConsole();
                    cfg.AddLog4Net();
                });
                collection.AddMemoryCache();
                provider = collection.BuildServiceProvider();
            }
        }

        public static T GetService<T>()
        {
            if (provider == null) throw new NullReferenceException("Need run ConfigureServiceProvider first");
            return provider.GetService<T>();
        }
        public static IEnumerable<T> GetServices<T>()
        {
            if (provider == null) throw new NullReferenceException("Need run ConfigureServiceProvider first");
            return provider.GetServices<T>();
        }
    }
}
