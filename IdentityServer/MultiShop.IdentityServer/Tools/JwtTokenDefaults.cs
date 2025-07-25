namespace MultiShop.IdentityServer.Tools
{
    public class JwtTokenDefaults
    {
        public const string ValidAudience = "http://localhost"; // Token geçerli olacak audience (kullanıcı kitlesi) adresi
        public const string ValidIssuer = "http://localhost"; // Token'ın geçerli olduğu issuer (yayıncı) adresi
        public const string Key = "MultiShop..0102030405Asp.NetCore6.0.28*/*+-"; // Token oluşturulurken kullanılacak gizli anahtar
        public const int ExpireMinutes = 60; // Token'ın geçerlilik süresi (dakika cinsinden)
    }
}
