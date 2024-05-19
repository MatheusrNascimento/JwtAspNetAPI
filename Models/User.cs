namespace JwtAspNet.Models
{
    public record User(long Id, 
    string Email, 
    string Password, 
    string[] Roles
    );
}