using System.Security.Claims;
using JwtAspNet.Models;
using JwtAspNet.Services;
using JwtAspNetAPI.PipelineExtensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.pipilineExtensionsMiddlewere();

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
    id = user.Claims.FirstOrDefault(x => x.Type == "id").Value,
    name = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value,
    email = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
    GivenName = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
    image = user.Claims.FirstOrDefault(x => x.Type == "image").Value

}).RequireAuthorization()
  .WithOpenApi();

app.MapGet("/admin", () => "Usuario com acesso!")
.RequireAuthorization("Admin")
.WithOpenApi();


app.Run();
