using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MultiShop.DtoLayer.CatalogDtos.CategoryDtos;
using MultiShop.DtoLayer.CatalogDtos.ProductDtos;
using Newtonsoft.Json;
using System.Text;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
	[Area("Admin")]
	[AllowAnonymous]
	[Route("Admin/Product")]
	public class ProductController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ProductController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}
		[Route("Index")]
		public async Task<IActionResult> Index()
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7070/api/Products");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
				var values = JsonConvert.DeserializeObject<List<ResultProductDto>>(jsonData); // json formatını metne çevir
				return View(values);
			}
			return View();
		}
		[Route("ProductListWithCategory")]
		public async Task<IActionResult> ProductListWithCategory()
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7070/api/Products/ProductListWithCategory");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
				var values = JsonConvert.DeserializeObject<List<ResultProductWithCategoryDto>>(jsonData); // json formatını metne çevir
				return View(values);
			}
			return View();
		}
		[Route("CreateProduct")]
		[HttpGet]
		public async Task<IActionResult> CreateProduct()
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Listesi";
			ViewBag.v0 = "Ürün İşlemleri";

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7070/api/Categories");
			var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
			var values = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData); // json formatını metne çevir
			// DropdownList için SelectListItem oluştur
			List<SelectListItem> categoryValues = (from x in values
												   select new SelectListItem
												   {
													   Text = x.CategoryName, // DropdownList'te gözükecek olan değer
													   Value = x.CategoryID   // DropdownList'ten seçilen değer
												   }).ToList();
			ViewBag.CategoryValues = categoryValues;

			return View();
		}
		[HttpPost]
		[Route("CreateProduct")]
		public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var json = JsonConvert.SerializeObject(createProductDto);
			StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
			var responseMessage = await client.PostAsync("https://localhost:7070/api/Products", data);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Product", new { area = "Admin" });
			}
			return View();
		}
		[Route("DeleteProduct/{id}")]
		public async Task<IActionResult> DeleteProduct(string id)
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.DeleteAsync("https://localhost:7070/api/Products?id=" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Product", new { area = "Admin" });
			}
			return View();
		}
		[Route("UpdateProduct/{id}")]
		[HttpGet]
		public async Task<IActionResult> UpdateProduct(string id)
		{
			ViewBag.v1 = "Ana Sayfa";
			ViewBag.v2 = "Ürünler";
			ViewBag.v3 = "Ürün Güncelleme Sayfası";
			ViewBag.v0 = "Ürün İşlemleri";

			var client1 = _httpClientFactory.CreateClient();
			var responseMessage1 = await client1.GetAsync("https://localhost:7070/api/Categories");
			var jsonData1 = await responseMessage1.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
			var values1 = JsonConvert.DeserializeObject<List<ResultCategoryDto>>(jsonData1); // json formatını metne çevir
																						   // DropdownList için SelectListItem oluştur
			List<SelectListItem> categoryValues1 = (from x in values1
												   select new SelectListItem
												   {
													   Text = x.CategoryName, // DropdownList'te gözükecek olan değer
													   Value = x.CategoryID   // DropdownList'ten seçilen değer
												   }).ToList();
			ViewBag.CategoryValues = categoryValues1;

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("https://localhost:7070/api/Products/" + id);
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync(); // gelen veriyi string formatta oku
				var values = JsonConvert.DeserializeObject<UpdateProductDto>(jsonData); // json formatını metne çevir
				return View(values);
			}
			return View();
		}
		[Route("UpdateProduct/{id}")]
		[HttpPost]
		public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto)
		{
			var client = _httpClientFactory.CreateClient();
			var json = JsonConvert.SerializeObject(updateProductDto);
			StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
			var responseMessage = await client.PutAsync("https://localhost:7070/api/Products", data);
			if (responseMessage.IsSuccessStatusCode)
			{
				return RedirectToAction("Index", "Product", new { area = "Admin" });
			}
			return View();
		}
	}
}
