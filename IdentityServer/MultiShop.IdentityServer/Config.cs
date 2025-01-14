﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Models;
using System.Collections.Generic;

namespace MultiShop.IdentityServer
{
    public static class Config
    {
        // Property , IEnumerable -> Koleksiyon
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] // örnek atadık
        {
            // Her mikroservis için o mikroservise erişim sağlanacak bir key belirliyoruz.
            // ResourceCatalog ismindeki key'e sahip olan bir mikroservis kullanıcısı CatalogFullPermission işlemini gerçekleştirebilecek.
            new ApiResource("ResourceCatalog"){Scopes = {"CatalogFullPermission", "CatalogReadPermission"}}
        };

        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
        {
            // Token'ının aldığımız kullanıcının hangi yetkilere erişim sağlayacağını belirledik
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        { 
            // CatalogFullPermission'a sahip kişinin yapabileceği işlemler
            new ApiScope("CatalogFullPermission", "Full authority for catalog operations"),
            new ApiScope("CatalogReadPermission", "Reading authority for catalog operations")
        };
    }
}