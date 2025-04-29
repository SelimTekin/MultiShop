using Microsoft.AspNetCore.Mvc;

namespace MultiShop.WebUI.Areas.Admin.Controllers
{
    public class ProductImageController : Controller
    {
        public IActionResult ProductImageDetail(string id)
        {
            return View();
        }
    }
}
