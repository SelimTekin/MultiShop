using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Controllers
{
	public class ProductListController : Controller
	{
		public IActionResult Index(string categoryId)
		{
			ViewBag.categoryId = categoryId;
			return View();
		}

		public IActionResult ProductDetail(string id)
		{
			ViewBag.productId = id;
            return View();
		}
	}
}
