using MultiShop.DtoLayer.DiscountDtos;

namespace MultiShop.WebUI.Services.DiscountServices
{
    public class DiscountService : IDiscountService
    {
        private readonly HttpClient _httpClient;
        public DiscountService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GetDiscountDetailCodeByCode> GetDiscountCode(string code)
        {
            //var responseMessage = await _httpClient.GetAsync($"discounts/GetCodeDetailByCode/{code}");
            var responseMessage = await _httpClient.GetAsync("http://localhost:7071/api/discounts/GetCodeDetailByCode?code=" + code);
            var values = await responseMessage.Content.ReadFromJsonAsync<GetDiscountDetailCodeByCode>();
            return values;
        }
    }
}
