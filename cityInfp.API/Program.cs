using cityInfp.API;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.StaticFiles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //options.OutputFormatters.RemoveType(typeof(SystemTextJsonOutputFormatter));
    //options.OutputFormatters.Add(new XmlDataContractSerializerOutputFormatter());

    options.ReturnHttpNotAcceptable = true;
}).AddXmlDataContractSerializerFormatters();

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
