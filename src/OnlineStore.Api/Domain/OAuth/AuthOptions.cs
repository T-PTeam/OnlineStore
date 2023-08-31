using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OnlineStore.Api.Domain.OAuth
{
    public class AuthOptions
    {
        public const string ISSUER = "OnlineStore";
        public const string AUDIENCE = "StoreClient";
        const string KEY = "ourtpteamkey421";
        public static SymmetricSecurityKey GetSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }

}
