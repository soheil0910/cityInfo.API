using CityInfo.API;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;
using Serilog;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration.GetConnectionString("FileLog_Address"), rollingInterval: RollingInterval.Day)
    .CreateLogger();




// Add services to the container.


builder.Host.UseSerilog();

builder.Services.AddControllers()
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();

//    (options =>
//{
//    //options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
//    //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

//    //options.ReturnHttpNotAcceptable = true;
//});
///////////////////////////////////////////////////////

//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseMiddleware<redirrect>();

app.UseHttpsRedirection();
//app.UseRouting();
app.UseAuthorization();

//app.UseEndpoints(endpoints =>

//    endpoints.MapControllers()
//);



app.MapControllers();







app.Run();
