using MultiShop.Basket.Dtos;
using MultiShop.Basket.Settings;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MultiShop.Basket.Services
{
    public class BasketService : IBasketService
    {
        private readonly RedisService _redisService;

        public BasketService(RedisService redisService)
        {
            _redisService = redisService;
        }

        public async Task DeleteBasket(string userId)
        {
            // KeyDeleteAsync -> redise özgü metod
            await _redisService.GetDb().KeyDeleteAsync(userId);
        }

        public async Task<BasketTotalDto> GetBasket(string userId)
        {
            // id'ye göre sepet getirecek (StringGetAsync -> redise özgü)
            var existbasket = await _redisService.GetDb().StringGetAsync(userId);
            return JsonSerializer.Deserialize<BasketTotalDto>(existbasket);
        }

        public async Task SaveBasket(BasketTotalDto basketTotalDto)
        {
            // key - value şeklinde oluyor parametreler (StringSetAsync -> redise özgü metod)
            await _redisService.GetDb().StringSetAsync(basketTotalDto.UserId, JsonSerializer.Serialize(basketTotalDto));
        }
    }
}
