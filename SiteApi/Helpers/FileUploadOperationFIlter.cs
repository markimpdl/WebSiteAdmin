using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Http;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // Busca o primeiro parâmetro que é um DTO com propriedades
        var dtoParam = context.MethodInfo
            .GetParameters()
            .FirstOrDefault(p => p.ParameterType.GetProperties().Any());

        if (dtoParam == null)
            return;

        // Verifica se há pelo menos um IFormFile
        var hasFile = dtoParam.ParameterType
            .GetProperties()
            .Any(p => p.PropertyType == typeof(IFormFile));

        if (!hasFile)
            return; // se não tem arquivo, não aplica multipart/form-data

        var schema = new OpenApiSchema
        {
            Type = "object",
            Properties = { }
        };

        foreach (var prop in dtoParam.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var isFile = prop.PropertyType == typeof(IFormFile);

            schema.Properties.Add(prop.Name, new OpenApiSchema
            {
                Type = isFile ? "string" : "string",
                Format = isFile ? "binary" : null
            });
        }

        operation.RequestBody = new OpenApiRequestBody
        {
            Content = {
                ["multipart/form-data"] = new OpenApiMediaType
                {
                    Schema = schema
                }
            }
        };
    }
}
