// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace PhoneBook.Security
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId
                {
                    DisplayName = "Your user identifier",
                    Required = true,
                    UserClaims =
                    {
                        "sub"
                    }
                },
                new IdentityResources.Profile
                {
                    DisplayName = "User profile",
                    Description =  "Your user profile information (first name, last name, etc.)",
                    Emphasize = true,
                    UserClaims =
                    {
                        "name",
                        "family_name",
                        "given_name",
                        "middle_name",
                        "preferred_username",
                        "profile",
                        "picture",
                        "website",
                        "gender",
                        "birthdate",
                        "zoneinfo",
                        "locale",
                        "updated_at",
                        "email"
                    }
                },
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                new ApiResource
                {
                    Name = "Phonebook api",
                    DisplayName = "My API #1",

                    Scopes =
                    {
                        new Scope()
                        {
                            Name = "phonebook_api"
                        }
                    }
                }
            };


        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // client credentials flow client
                new Client
                {
                    ClientId = "client",
                    ClientName = "Client Credentials Client",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },

                    AllowedScopes = { "api1" }
                },

                // MVC client using code flow + pkce
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",

                    AllowedGrantTypes = GrantTypes.CodeAndClientCredentials,
                    RequirePkce = true,
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },

                    RedirectUris = { "http://localhost:5003/signin-oidc" },
                    FrontChannelLogoutUri = "http://localhost:5003/signout-oidc",
                    PostLogoutRedirectUris = { "http://localhost:5003/signout-callback-oidc" },

                    AllowOfflineAccess = true,
                    AllowedScopes = { "openid", "profile", "api1" }
                },

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "phonebook_api" },
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "http://localhost:4200/home",
                        "http://phonebook.btrc.local/home",
                    },

                    PostLogoutRedirectUris = 
                    {
                        "http://localhost:4200/home",
                         "http://phonebook.btrc.local/home",
                    },

                    AllowedCorsOrigins = 
                    { 
                        "http://localhost:4200",
                        "http://phonebook.btrc.local"
                    }
                },

                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "phonebook_api" },
                    AllowAccessTokensViaBrowser = true,

                    RedirectUris =
                    {
                        "https://localhost:44312/swagger/oauth2-redirect.html",
                    },

                    AllowedCorsOrigins = { "https://localhost:44312" },
                },
            };
    }
}