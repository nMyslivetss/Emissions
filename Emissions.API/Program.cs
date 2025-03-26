using Emissions.API.Middlewares;
using Emissions.API.Swagger;
using Emissions.Application.Interfaces;
using Emissions.Application.Mapping;
using Emissions.Application.Services;
using Emissions.Infrastructure.Clients;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(EmissionMappingProfile).Assembly);
builder.Services.AddHttpClient();
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
});

builder.Services.AddScoped<IEmissionService, EmissionService>();
builder.Services.AddScoped<IBackgroundEmissionsClient, BackgroundEmissionsClient>();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Emission API", Version = "1.0" });


    options.DocInclusionPredicate((docName, apiDesc) =>
    {
        return docName == "v1";
    });

    options.OperationFilter<RemoveVersionFromParameter>();
    options.DocumentFilter<ReplaceVersionWithExactValueInPath>();

    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Emission API (v1 & v2)");
    });
}
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
