using System;
using System.Linq;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

public class LowerCaseSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema?.Properties == null)
            return;

        foreach (var property in schema.Properties.ToList())
        {
            var lowercaseKey = Char.ToLowerInvariant(property.Key[0]) + property.Key.Substring(1);
            if (property.Key != lowercaseKey)
            {
                schema.Properties.Remove(property.Key);
                schema.Properties.Add(lowercaseKey, property.Value);
            }
        }
    }
}