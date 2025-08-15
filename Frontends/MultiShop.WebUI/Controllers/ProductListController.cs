using Microsoft.AspNetCore.Mvc;
using MultiShop.DtoLayer.CommentDtos;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.WebUI.Controllers
{
	public class ProductListController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductListController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public IActionResult Index(string id)
		{
            ViewBag.categoryId = id;
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürünler";
            ViewBag.directory2 = "Ürün Listesi";
			return View();
		}

		public IActionResult ProductDetail(string id)
		{
			ViewBag.productId = id;
            ViewBag.directory1 = "Ana Sayfa";
            ViewBag.directory2 = "Ürün Listesi";
            ViewBag.directory2 = "Ürün Detayları";
            return View();
		}
		[HttpGet]
		public PartialViewResult AddComment()
		{
            return PartialView();
		}
		[HttpPost]
		public async Task<IActionResult> AddComment(CreateCommentDto createCommentDto)
		{
			createCommentDto.ProductId = "676c5f897a51ae9ff33adc2d";
			createCommentDto.ImageUrl = "test";
			createCommentDto.Rating = 1;
			createCommentDto.CreatedDate = DateTime.Parse(DateTime.Now.ToShortDateString());
			createCommentDto.Status = false;

			var client = _httpClientFactory.CreateClient();
			var jsonData = JsonConvert.SerializeObject(createCommentDto);
			StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7178/api/Comments", stringContent);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Default");
			}
			return View();
		}
	}
}
