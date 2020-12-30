// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace PhoneBook.Security
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> Ids =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId
                {
                    DisplayName = "Ваш идентификатор пользователя",
                    Required = true,
                    UserClaims =
                    {
                        "sub"
                    }
                },
                new IdentityResources.Profile
                {
                    DisplayName = "Профиль пользователя",
                    Description =  "Информация о вашем профиле пользователя (имя, фамилия и т. д.)",
                    Emphasize = true,
                    UserClaims =
                    {
                        "name",
                        "email",
                        "role"
                    }
                }
            };


        public static IEnumerable<ApiResource> Apis =>
            new ApiResource[]
            {
                    new ApiResource()
                    {
                        Name = "Phonebook api",
                        DisplayName = "Phonebook api",

                        Scopes =
                        {
                            new Scope()
                            {
                                Name = "phonebook_api",
                                DisplayName = "Доступ к телефонному справочнику"
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

                    AllowedScopes = { "phonebook_api" }
                },

                // SPA client using code flow + pkce
                new Client
                {
                    ClientId = "spa",
                    ClientName = "SPA Client",

                    AlwaysIncludeUserClaimsInIdToken = true,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    AlwaysSendClientClaims = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowOfflineAccess = true,   

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "profile", "phonebook_api" },
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
                    },
                },

                new Client
                {
                    ClientId = "swagger",
                    ClientName = "Swagger Client",

                    AllowedGrantTypes = GrantTypes.Implicit,
                    AllowedScopes = { "openid", "phonebook_api" },
                    AllowAccessTokensViaBrowser = true,
                    AlwaysIncludeUserClaimsInIdToken = true,

                    RedirectUris =
                    {
                        "https://localhost:44312/swagger/oauth2-redirect.html",
                    },

                    AllowedCorsOrigins = { "https://localhost:44312" },
                    AlwaysSendClientClaims = true,
                },
            };
    }
}