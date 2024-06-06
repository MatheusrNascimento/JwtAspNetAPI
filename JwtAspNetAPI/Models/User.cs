namespace JwtAspNet.Models
{
    public record User(long Id,
    string Name,
    string Email,
    string Image,
    string Password, 
    string[] Roles
    );
}