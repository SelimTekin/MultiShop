using Microsoft.Extensions.Options;
using MultiShop.DtoLayer.IdentityDtos.LoginDtos;
using MultiShop.WebUI.Services.Interface;
using MultiShop.WebUI.Settings;
using IdentityModel.Client;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace MultiShop.WebUI.Services.Concrete
{
    public class IdentityService : IIdentityService
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ClientSettings _clientSettings;

        // IOptions<ClientSettings> is used to inject the settings from the configuration (in appsettings.json file)
        public IdentityService(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IOptions<ClientSettings> clientSettings)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _clientSettings = clientSettings.Value;
        }

        public async Task<bool> SignIn(SignUpDto signUpDto)
        {
            var discoveryEndPoint = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest
            {
                Address = "http://localhost:5001",
                Policy = new DiscoveryPolicy
                {
                    RequireHttps = false // Set to true in production
                }
            });

            var passwordTokenRequest = new PasswordTokenRequest
            {
                ClientId = _clientSettings.MultiShopManagerClient.ClientId,
                ClientSecret = _clientSettings.MultiShopManagerClient.ClientSecret,
                UserName = signUpDto.Username,
                Password = signUpDto.Password,
                Address = discoveryEndPoint.TokenEndpoint
            };
            var token = await _httpClient.RequestPasswordTokenAsync(passwordTokenRequest);

            var userInfoRequest = new UserInfoRequest
            {
                Token = token.AccessToken,
                Address = discoveryEndPoint.UserInfoEndpoint
            };

            var userValues = await _httpClient.GetUserInfoAsync(userInfoRequest);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(userValues.Claims, CookieAuthenticationDefaults.AuthenticationScheme, "name", "role");
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            var authenticationProperties = new AuthenticationProperties();

            authenticationProperties.StoreTokens(new List<AuthenticationToken>
            {
                new AuthenticationToken{
                    Name = OpenIdConnectParameterNames.AccessToken,
                    Value = token.AccessToken
                },
                new AuthenticationToken{
                    Name = OpenIdConnectParameterNames.RefreshToken,
                    Value = token.RefreshToken
                },
                new AuthenticationToken{
                    Name = OpenIdConnectParameterNames.ExpiresIn,
                    Value = DateTime.Now.AddSeconds(token.ExpiresIn).ToString()
                }
            });

            authenticationProperties.IsPersistent = false; // Beni hatırla seçeneği (her defasında giriş yapılacak)

            await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, authenticationProperties);

            return true;
        }
    }
}
