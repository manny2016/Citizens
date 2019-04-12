

using Microsoft.Extensions.DependencyInjection;

namespace Citizens.Core
{
    public static class ServiceExtension
    {
        public static ServiceCollection AddCoreServices(this ServiceCollection collection)
        {            
            return collection;
        }
    }
}
