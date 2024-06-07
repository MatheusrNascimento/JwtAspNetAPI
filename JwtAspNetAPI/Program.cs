using System.Security.Claims;
using JwtAspNet.Models;
using JwtAspNet.Services;
using JwtAspNetAPI.Extensions;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigurationMiddlewere();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de authenticação JWT Bearer V1");
        c.DocumentTitle = "Authenticação Jwt Bearer";
    });
}

app.MapPost("/login", (TokenService service, [FromBody] User user) =>
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
  .InfoHeaders();

app.MapGet("/admin", () => "Usuario com acesso!")
.RequireAuthorization("Admin")
.InfoHeaders();


app.Run();
