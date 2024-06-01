using System.Security.Claims;
using JwtAspNet.Models;
using JwtAspNet.Services;
using JwtAspNetAPI.Extensions;
using JwtAspNetAPI.PipelineExtensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.pipelineExtensionsMiddlewere();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de authenticação JWT Barer V1");
    });
}

app.MapGet("/login", (TokenService service, [FromBody] User user) =>
{
    return service.CreateToken(user);
}).WithOpenApi();

app.MapGet("/restrict", (ClaimsPrincipal user) => new
{
    id = user.Id(),
    name = user.Name(),
    email = user.Email(),
    GivenName = user.GivenName(),
    image = user.Image()

}).RequireAuthorization()
  .WithOpenApi();

app.MapGet("/admin", () => "Usuario com acesso!")
.RequireAuthorization("Admin")
.WithOpenApi();


app.Run();
