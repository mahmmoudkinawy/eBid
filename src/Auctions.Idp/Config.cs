using Duende.IdentityServer.Models;

namespace Auctions.Idp;
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
            new("auctionClient", "Auctions Client-Side App")
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            new()
            {
                // Only for development
                ClientId = "postman",
                ClientName = "postman",
                AllowedScopes = { "openid", "profile", "auctionClient" },
                RedirectUris = { "https://www.getpostman.com/oauth2/callback" },
                ClientSecrets = { new Secret("NotASecret".Sha256()) },
                AllowedGrantTypes = { GrantType.ResourceOwnerPassword }
            },
            new()
            {
                ClientId = "nextApp",
                ClientName = "nextApp",
                ClientSecrets = { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                RequirePkce = false,
                RedirectUris = { "http://localhost:3000/api/auth/callback/id-server" },
                AllowOfflineAccess = true,
                AllowedScopes = { "openid", "profile", "auctionClient" },
                AccessTokenLifetime = 3600 * 24 * 30
            }
        };
}
