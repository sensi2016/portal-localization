using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portal.Api.Infrastructure
{
    public class SwaggerAddRequiredHeaderParameter : IOperationFilter
    {
   

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();


            //operation.Parameters.Add(new OpenApiParameter()
            //{
            //    Name = "Authorization",
            //    In = ParameterLocation.Header,
            //    Required = false,
            //   Schema = new OpenApiSchema
            //   {
            //       Type = "String",
            //   } 
            //});

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "SectionId",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                }
            });

            operation.Parameters.Add(new OpenApiParameter()
            {
                Name = "Accept-Language",
                In = ParameterLocation.Header,
                Required = false,
                Schema = new OpenApiSchema
                {
                    Type = "String",
                }
            });
        }
    }
}
