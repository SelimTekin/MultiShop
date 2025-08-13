// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
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
            new ApiResource("ResourceCatalog"){Scopes = {"CatalogFullPermission", "CatalogReadPermission"}},
            new ApiResource("ResourceDiscount"){Scopes = {"DiscountFullPermission"}},
            new ApiResource("ResourceOrder"){Scopes = {"OrderFullPermission"}},
			new ApiResource("ResourceCargo"){Scopes = {"CargoFullPermission"}},
			new ApiResource("ResourceBasket"){Scopes = {"BasketFullPermission"}},
			new ApiResource("ResourceOcelot"){Scopes = {"OcelotFullPermission"}},
			new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
		};

		public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[]
		{
            // Token'ını aldığımız kullanıcının hangi yetkilere erişim sağlayacağını belirledik
            new IdentityResources.OpenId(),
			new IdentityResources.Email(),
			new IdentityResources.Profile(),
		};

		public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
		{ 
            // CatalogFullPermission'a sahip kişinin yapabileceği işlemler
            new ApiScope("CatalogFullPermission", "Full authority for catalog operations"),
			new ApiScope("CatalogReadPermission", "Reading authority for catalog operations"),
			new ApiScope("DiscountFullPermission", "Full authority for catalog operations"),
			new ApiScope("OrderFullPermission", "Full authority for catalog operations"),
			new ApiScope("CargoFullPermission", "Full authority for cargo operations"),
			new ApiScope("BasketFullPermission", "Full authority for basket operations"),
			new ApiScope("CommentFullPermission", "Full authority for comment operations"),
			new ApiScope("PaymentFullPermission", "Full authority for payment operations"),
			new ApiScope("ImageFullPermission", "Full authority for image operations"),
			new ApiScope("OcelotFullPermission", "Full authority for ocelot operations"),
			new ApiScope(IdentityServerConstants.LocalApi.ScopeName)
		};

		public static IEnumerable<Client> Clients => new Client[]
		{
			// Visitor -> Ziyaretçilerin sahip olacağı izinler
			new Client
			{
				ClientId="MultiShopVisitorId",
				ClientName="Multi Shop Visitor User",
				AllowedGrantTypes=GrantTypes.ClientCredentials, // Neye izin verildiği ile ilgili bir property -> Kimlik işlemleri için kullanacağımız property
				ClientSecrets={new Secret("multishopsecret".Sha256())}, // şifre
				AllowedScopes={ "CatalogReadPermission", "CatalogFullPermission", "CommentFullPermission", "ImageFullPermission", "OcelotFullPermission", IdentityServerConstants.LocalApi.ScopeName },
                AllowAccessTokensViaBrowser = true // Tarayıcı üzerinden erişime izin veriyoruz
			},

			// Manager
			new Client
			{
				ClientId="MultiShopManagerId",
				ClientName="Multi Shop Manager User",
				AllowedGrantTypes=GrantTypes.ResourceOwnerPassword, // Giriş yapan kullanıcının şifresine göre çalışsın
				ClientSecrets={new Secret("multishopsecret".Sha256())}, // şifre
				AllowedScopes={ "CatalogReadPermission", "CatalogFullPermission", "BasketFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImageFullPermission", "OcelotFullPermission",
                IdentityServerConstants.LocalApi.ScopeName, // yereldeki apiden scope'un adına ulaş
				IdentityServerConstants.StandardScopes.Email,
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile, }
			},

			// Admin
			new Client
			{
				ClientId="MultiShopAdminId",
				ClientName="Multi Shop Admin User",
				AllowedGrantTypes=GrantTypes.ResourceOwnerPassword, // Giriş yapan kullanıcının şifresine göre çalışsın
				ClientSecrets={new Secret("multishopsecret".Sha256())}, // şifre
				AllowedScopes={  "CatalogFullPermission", "CatalogReadPermission", "DiscountFullPermission", "OrderFullPermission", "CargoFullPermission", "BasketFullPermission", "CommentFullPermission", "PaymentFullPermission", "ImageFullPermission", "OcelotFullPermission",
                IdentityServerConstants.LocalApi.ScopeName, // yereldeki apiden scope'un adına ulaş
				IdentityServerConstants.StandardScopes.Email,
				IdentityServerConstants.StandardScopes.OpenId,
				IdentityServerConstants.StandardScopes.Profile,
				},
				AccessTokenLifetime=600 // 600 saniye (10 dk) token süresi
			}

		};
	}
}