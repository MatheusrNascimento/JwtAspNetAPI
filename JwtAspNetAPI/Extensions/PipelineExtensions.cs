using System.Text;
using JwtAspNet;
using JwtAspNet.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace JwtAspNetAPI.Extensions
{
    public static class PipelineExtensions
    {
        public static  IServiceCollection ConfigurationMiddlewere(this IServiceCollection services)
        {
            services.AddTransient<TokenService>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.PrivateKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddAuthorization(x =>
            {
                x.AddPolicy("Admin", p => p.RequireRole("admin"));
            });

            return services;
        }

        public static void InfoHeaders(this RouteHandlerBuilder appEndPoint)
        {
            appEndPoint.WithOpenApi(operation => new(operation)
            {
                Parameters = new List<OpenApiParameter>
                {
                    new OpenApiParameter 
                    {
                        Name = "Authentication",
                        In = ParameterLocation.Header,
                        Description = "Token de autenticação",
                        Schema = new OpenApiSchema
                        {
                            Type = "Token Jwt Bearer",
                        }
                    }
                },
            });
        }
    }
}