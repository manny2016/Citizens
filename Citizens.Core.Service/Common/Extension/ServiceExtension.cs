


namespace Citizens.Core
{
    using Microsoft.Extensions.DependencyInjection;

    public static class ServiceExtension
    {
        public static ServiceCollection AddCoreServices(this ServiceCollection collection)
        {
            collection.AddSingleton(typeof(Counter), new Counter());
            return collection;
        }
    }
}
