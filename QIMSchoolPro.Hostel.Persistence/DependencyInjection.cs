using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Qface.Application.Shared.Common.Interfaces;
using QIMSchoolPro.Hostel.Infrastructure.Identity;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Persistence.Interfaces;
using QIMSchoolPro.Hostel.Persistence.Repositories;


namespace QIMSchoolPro.Hostel.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
        {
            services.RegisterDbContext(configuration)
                .RegisterRepositories();
            return services;
        }

        private static IServiceCollection RegisterDbContext(this IServiceCollection services,
        IConfiguration configuration)
        {
            var assemblyName = typeof(HostelDbContext).Assembly.FullName;

            services.AddDbContext<HostelDbContext>(options =>
               options.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   b =>
                   {
                       b.MigrationsAssembly(assemblyName);
                   }));
            return services;
        }

        private static IServiceCollection RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBuildingRepository, BuildingRepository>();
            services.AddScoped<IFloorRepository, FloorRepository>();
            services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();
            services.AddScoped<IRoomRepository, RoomRepository>();
            services.AddScoped<IBedRepository, BedRepository>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IRoomTypeAmenityRepository, RoomTypeAmenityRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IAcademicConfigurationRepository, AcademicConfigurationRepository>();
            services.AddScoped<IRoomFilterRepository, RoomFIlterRepository>();



            services.AddTransient<IIdentityService, IdentityService>();
           
         

            return services;
        }
    }
}
