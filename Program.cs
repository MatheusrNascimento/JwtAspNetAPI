using JwtAspNet.Models;
using JwtAspNet.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<TokenService>();

var app = builder.Build();

app.MapGet("/", (TokenService service) => {
    User user = new User(Id: 1, 
    Name: "Matheus Rodrigues", 
    Email: "srabacaxifrustado@gmail.com", 
    Image: "https://imagem", 
    Password: "senha123",
    Roles: new[] {"studant", "premium"});

    return service.CreateToken(user);
});

app.Run();
