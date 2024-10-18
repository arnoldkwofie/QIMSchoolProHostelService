using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using QIMSchoolPro.Hostel.Persistence;
using QIMSchoolPro.Hostel.Application;
using QIMSchoolPro.Hostel.Processors;

namespace QIMSchoolProHostelService
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services,
                  IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            services.InstallDefaults()
                .AddHttpContextAccessor()
                .InstallSwagger(configuration)
                .AddCore(configuration)
                .AddInfrastructure(configuration)
                .AddProcessors()
                .RegisterAutoMapper()
                .AddHttpClient();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuerSigningKey = true,
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:Secret"])),
                     ValidateIssuer = false,
                     ValidateAudience = false
                 };
             });

        }
        private static IServiceCollection InstallDefaults(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
            services.AddEndpointsApiExplorer()
                .InstallCors();
            return services;
        }

        private static IServiceCollection InstallCors(this IServiceCollection services)
        {
   //         services.AddCors(options =>
   //         {
   //             options.AddDefaultPolicy(opts =>
   //             {
   //                 opts.AllowAnyOrigin()
   //                 .AllowAnyMethod()
   //                 .AllowAnyHeader();
   //             });

			//	//options.AddDefaultPolicy(opts =>
			//	//{
			//	//    opts.WithOrigins("http://localhost:3000")
			//	//        .AllowAnyMethod()
			//	//        .AllowAnyHeader();
			//	//});

			
			//});

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder => builder
					.SetIsOriginAllowed((host) => true)
					.AllowAnyMethod()
					.AllowAnyHeader()
					.AllowCredentials());
			});


			return services;
        }

        private static IServiceCollection InstallSwagger(this IServiceCollection services,
           IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "UMaT Hostel API",
                    Contact = new OpenApiContact
                    {
                        Email = "akwofie1@umat.edu.gh",
                        Name = "",
                        Url = new Uri("https://umat.edu.gh")
                    },
                    Description = "An API for Hostel Management."
                });

                options.CustomOperationIds(description =>
                    description.TryGetMethodInfo(out var methodInfo)
                        ? methodInfo.Name : description.RelativePath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization using the bearer scheme",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {new OpenApiSecurityScheme{Reference = new OpenApiReference()
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }}, new List<string>()}
            });

                
            });
            return services;
        }


    }
}
