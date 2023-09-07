using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace OnlineStore.IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new ApiScope("OnlineStoreApi"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new Client
            {
                ClientId = "online_store_client_api",
                ClientSecrets = {new Secret("online_store_client_secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = {"https://localhost:6001"},
                AllowedScopes =
                {
                    "OnlineStoreApi",
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                }
            }
        };
}
