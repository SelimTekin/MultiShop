namespace MultiShop.Basket.LoginServices
{
    public class LoginService : ILoginServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // sub -> token'dan gelecek userId'yi barındırıyor
        public string GetUserId => _httpContextAccessor.HttpContext.User.FindFirst("sub").Value;
    }
}
