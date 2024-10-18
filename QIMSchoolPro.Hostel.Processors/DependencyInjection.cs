using Microsoft.Extensions.DependencyInjection;
using QIMSchoolPro.Hostel.Processors.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace QIMSchoolPro.Hostel.Processors
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        {
            var assemblies = Assembly.GetExecutingAssembly();
            services.AddAutoMapper(assemblies);

            return services;

        }
        public static IServiceCollection AddProcessors(this IServiceCollection services)
        {

            var attributes = typeof(ProcessorBase);
            var assemblies = attributes.Assembly;
            var definedTypes = assemblies.DefinedTypes;

            var processors = definedTypes.Where(type => type.GetTypeInfo().GetCustomAttribute<ProcessorBase>() != null)
                .ToList();


            foreach (var processor in processors)
                services.AddScoped(processor);

            return services;
        }
    }
}
