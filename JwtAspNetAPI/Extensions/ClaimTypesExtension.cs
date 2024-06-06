using System.Security.Claims;

namespace JwtAspNetAPI.Extensions
{
    public static class ClaimTypesExtension
    {
        public static int Id(this ClaimsPrincipal user)
        {
            try
            {
                string id = user.Claims.FirstOrDefault(x => x.Type == "id")?.Value ?? "0";

                return int.Parse(id);
            }
            catch

            {
                return 0;
            }
        }

        public static string Name(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value ?? "Erro ao obter";
            }
            catch

            {
                return string.Empty;
            }
        }

        public static string Email(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? "Erro ao obter";
            }
            catch

            {
                return string.Empty;
            }
        }

        public static string GivenName(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value ?? "Erro ao obter";
            }
            catch

            {
                return string.Empty;
            }
        }
        
        public static string Image(this ClaimsPrincipal user)
        {
            try
            {
                return user.Claims.FirstOrDefault(x => x.Type == "image")?.Value ?? "Erro ao obter";
            }
            catch

            {
                return string.Empty;
            }
        }
    }
}