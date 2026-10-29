using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace LangScan.Patches;

public class ValidationSchemaFilter : ISchemaFilter
{
    public void Apply(IOpenApiSchema schema, SchemaFilterContext context)
    {
        if (schema.Properties is null || schema.Properties.Count == 0)
            return;

        var constructor = context.Type.GetConstructors()
            .MaxBy(c => c.GetParameters().Length);

        if (constructor is null)
            return;

        var parameters = constructor.GetParameters();

        foreach (var (propertyName, propertySchema) in schema.Properties)
        {
            var parameter = parameters.FirstOrDefault(p =>
                string.Equals(p.Name, propertyName, StringComparison.OrdinalIgnoreCase));

            if (parameter is null)
                continue;

            var validationAttributes = parameter.GetCustomAttributes<ValidationAttribute>(true);

            foreach (var attribute in validationAttributes)
            {
                ApplyValidationToSchema(propertySchema, attribute);
            }
        }
    }

    private static void ApplyValidationToSchema(IOpenApiSchema schema, ValidationAttribute attribute)
    {
        if (schema is not OpenApiSchema concreteSchema)
            return;

        switch (attribute)
        {
            case MaxLengthAttribute maxLength:
                concreteSchema.MaxLength = maxLength.Length;
                break;
            case MinLengthAttribute minLength:
                concreteSchema.MinLength = minLength.Length;
                break;
            case StringLengthAttribute stringLength:
                concreteSchema.MaxLength = stringLength.MaximumLength;
                concreteSchema.MinLength = stringLength.MinimumLength;
                break;
            case RangeAttribute range:
                concreteSchema.Minimum = Convert.ToString(range.Minimum, CultureInfo.InvariantCulture);
                concreteSchema.Maximum = Convert.ToString(range.Maximum, CultureInfo.InvariantCulture);
                break;
            case RegularExpressionAttribute regex:
                concreteSchema.Pattern = regex.Pattern;
                break;
            case EmailAddressAttribute:
                concreteSchema.Format = "email";
                break;
            default:
                throw new NotSupportedException($"Unhandled validation attribute '{attribute.GetType().Name}' on parameter.");
        }
    }
}
