namespace ThreadsBackend.Api.Infrastructure.Configurations;

using System.Reflection;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this IServiceCollection services, string serviceNamespace, Assembly assembly)
    {
        var serviceTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.Namespace == serviceNamespace);

        foreach (var serviceType in serviceTypes)
        {
            var implementedInterface = serviceType.GetInterface($"I{serviceType.Name}");
            if (implementedInterface is not null)
            {
                services.AddScoped(implementedInterface, serviceType);
            }
        }

        // var serviceTypes = assembly.GetTypes()
        //     .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces().Any(i => i.Name.StartsWith("I")))
        //     .ToList();
        //
        // foreach (var serviceType in serviceTypes)
        // {
        //     services.AddScoped(serviceType.GetInterfaces().First(i => i.Name.StartsWith("I")), serviceType);
        // }
    }
}
