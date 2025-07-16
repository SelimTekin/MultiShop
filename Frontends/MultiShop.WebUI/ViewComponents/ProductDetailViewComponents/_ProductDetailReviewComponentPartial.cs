using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;

namespace MultiShop.WebUI.ViewComponents.ProductDetailViewComponents
{
	public class _ProductDetailReviewComponentPartial : ViewComponent
	{
        private readonly IHttpClientFactory _httpClientFactory;
        public _ProductDetailReviewComponentPartial(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IViewComponentResult> InvokeAsync(string id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7178/api/Comments/CommentListByProductId?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
                var values = JsonConvert.DeserializeObject<List<ResultCommentDto>>(jsonData); // json formatını metne çevir
                return View(values);
            }
            return View();
        }
	}
}
