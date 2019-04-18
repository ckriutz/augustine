using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Augustine.IDP
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "CC05868E-DA05-4395-9160-437AEEE208D0",
                    Username = "kkraus",
                    Password = "pass@word1",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Kevin"),
                        new Claim("family_name", "Kraus"),
                    }
                },
                new TestUser
                {
                    SubjectId = "82C00D20-C3F7-41E8-A1D1-93AB88D6C1BE",
                    Username = "ckriutz",
                    Password = "pass@word1",
                    Claims = new List<Claim>
                    {
                        new Claim("given_name", "Casey"),
                        new Claim("family_name", "Kriutzfield"),
                    }
                }
            };
        }

        internal static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientName = "Augustine.Senator.Web",
                    ClientId = "augustinesenatorclient",
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    ClientUri = "https://localhost:44365",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    AllowAccessTokensViaBrowser = false,
                    RequireConsent = false,
                    AllowOfflineAccess = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44365/signin-oidc",
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44365/signout-callback-oidc",
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "senatorsapi"

                    },
                    AccessTokenLifetime = 60*60*2, // 2 hours
                    IdentityTokenLifetime= 60*60*2 // 2 hours
                },
                new Client
                {
                    ClientName = "Augustine.Senator.WebApi.Swagger.UI",
                    ClientId = "augustinesenatorwebapiswaggerui",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44329/oauth2-redirect.html"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44329/"
                    },
                    AllowedScopes =
                    {
                        "senatorsapi"
                    }
                },
                new Client
                {
                    ClientName = "Augustine.Senator.WebForms",
                    ClientId = "augustinesenatorclientwebforms",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:44383/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:44383/signout-callback-oidc"
                    },
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Address,
                        "roles",
                        "senatorsapi"

                    },
                    ClientSecrets =
                    {
                        new Secret("secretwebforms".Sha256())
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("senatorsapi","Senators API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Address(),
                new IdentityResource(
                    "roles",
                    "Your Role(s)",
                    new List<string>() {"role"})
            };
        }
    }
}
