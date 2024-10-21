using CityInfo.API;
using CityInfo.API.DbContexts;
using CityInfo.API.Repositoties;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using Serilog;



var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File(builder.Configuration["FileLog_Address"], rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


// Add services to the container.



builder.Services.AddControllers()
    //.AddNewtonsoftJson()
    .AddNewtonsoftJson(options =>
    {
        ////Error Include
        options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        
    })
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
builder.Services.AddTransient<IMailService, CloudMailService>();
builder.Services.AddSingleton<CitiesDataStore>();
//builder.Services.AddDbContext<CityInfoDbContext>(option =>
//{
//    option.UseSqlite("Data Source=CityInfo.db");
//    //option.UseSqlite("Data Source=192.168.1.1;Initial  Catalog=ImanDb;User ID=iman;Password=123");
//});

builder.Services.AddDbContext<CityInfoDbContext>(option =>
{
    option.UseSqlite(
        builder.Configuration.GetConnectionString("CityConnectionStringSqlLite")
        //builder.Configuration["ConnectionStrings:CityConnectionStringSqlLite"]
        //builder.Configuration["CityConnectionStringSqlLiteEnvironment"]//////Environment
        );
});
builder.Services.AddScoped<ICityInfoRepository, CityInfoRepository>();

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
