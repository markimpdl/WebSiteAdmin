using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Linq;
using System.Reflection;

public class FileUploadOperationFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var param = context.MethodInfo.GetParameters()
            .FirstOrDefault(p => p.ParameterType.GetProperties().Any());

        if (param == null) return;

        var schema = new OpenApiSchema
        {
            Type = "object",
            Properties = { }
        };

        foreach (var prop in param.ParameterType.GetProperties(BindingFlags.Public | BindingFlags.Instance))
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
