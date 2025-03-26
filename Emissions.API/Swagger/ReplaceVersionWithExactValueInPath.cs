using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Emissions.API.Swagger
{
    public class ReplaceVersionWithExactValueInPath : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            var updatedPaths = new OpenApiPaths();

            foreach (var (key, value) in swaggerDoc.Paths)
            {
                updatedPaths.Add(key.Replace("v{version}", $"v{swaggerDoc.Info.Version}"), value);
            }

            swaggerDoc.Paths = updatedPaths;
        }
    }
}
