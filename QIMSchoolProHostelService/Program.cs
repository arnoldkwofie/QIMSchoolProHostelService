using QIMSchoolPro.Hostel.Processors.Components.Messages;
using QIMSchoolPro.Hostel.Processors.Components;
using QIMSchoolPro.Hostel.Processors.Helpers;
using QIMSchoolPro.Hostel.Processors.Middlewares;
using QIMSchoolProHostelService;
using Akka.Actor;
using Akka.DI.Core;
using QIMSchoolPro.Hostel.Processors.Components.Actors;
using Autofac.Core;
using StackExchange.Redis;
using Serilog;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day) // Log to a file
    .CreateLogger();

builder.Host.UseSerilog();


builder.Services.RegisterServices(builder.Configuration, builder.Environment);
builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddLogging(logging =>
{
    logging.AddSerilog();
    logging.AddConsole();
   
    logging.SetMinimumLevel(LogLevel.Debug);
});





var app = builder.Build();

app.UseMiddleware<ErrorHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI(
    c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "Hostel Project");
    });


app.Use(async (context, next) =>
{
    context.Response.Headers.Add("Access-Control-Allow-Origin", "*"); 
    await next();
});

app.UseCors("CorsPolicy");
//app.UseCors("AllowAllOrigins");

//app.UseHttpsRedirection();

//app.Urls.Add("https://+:443");

app.MapControllers();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization(); 


app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<BookingHub>("/bookingHub");
    
});


TopLevelActors.RoomUpdateSubscriberActor.Tell(new BackgroundMessage());


app.Run();


