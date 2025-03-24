using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.OpenApi.Models;

namespace KingPriceDemo.WebApi.Common.Transformers
{
    public sealed class BearerSecuritySchemeTransformer(IAuthenticationSchemeProvider authenticationSchemeProvider, IConfiguration configuration) : IOpenApiDocumentTransformer
    {
        public async Task TransformAsync(OpenApiDocument document, OpenApiDocumentTransformerContext context, CancellationToken cancellationToken)
        {
            var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

            if (authenticationSchemes.Any(authScheme => authScheme.Name == "Bearer"))
            {
                var requirements = new Dictionary<string, OpenApiSecurityScheme>
                {
                    ["Bearer"] = new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\nEnter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer MyBearerToken\"",
                    }
                };
                document.Components ??= new OpenApiComponents();
                document.Components.SecuritySchemes = requirements;
            }

            document.Info = new()
            {
                Title = configuration.GetValue<string>("ApiSpecification:Title"),
                Version = configuration.GetValue<string>("ApiSpecification:Version"),
                Description = configuration.GetValue<string>("ApiSpecification:Description"),
                Contact = new()
                {
                    Name = configuration.GetValue<string>("ApiSpecification:Contact:Name"),
                    Email = configuration.GetValue<string>("ApiSpecification:Contact:Email")
                }
            };

            document.Servers = configuration.GetSection("ApiSpecification:Servers").Get<List<OpenApiServer>>();
        }
    }
}
