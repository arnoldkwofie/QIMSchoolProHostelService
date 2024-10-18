using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QIMSchoolPro.Hostel.Processors.Processors;

using System.Reflection;


namespace QIMSchoolPro.Hostel.Application
{
    public static  class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterProcessors();
            services.AddMediatR(opts =>
            {
                
                opts.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
            });
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly)
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters()
                .AddMemoryCache();
                
            return services;
        }

        //public static IServiceCollection RegisterAutoMapper(this IServiceCollection services)
        //{
        //    var assemblies = Assembly.GetExecutingAssembly();
        //    services.AddAutoMapper(assemblies);
        //    return services;

        //}
        public static IServiceCollection RegisterProcessors(this IServiceCollection services)
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
