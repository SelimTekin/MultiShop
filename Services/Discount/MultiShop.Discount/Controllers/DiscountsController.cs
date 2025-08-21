using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MultiShop.Discount.Dtos;
using MultiShop.Discount.Services;

namespace MultiShop.Discount.Controllers
{
	[Authorize]
	[Route("api/[controller]")]
	[ApiController]
	public class DiscountsController : ControllerBase
	{
		private readonly IDiscountService _discountService;

		public DiscountsController(IDiscountService discountService)
		{
			_discountService = discountService;
		}

		[HttpGet]
		public async Task<IActionResult> DiscountCouponList()
		{
			// await kullanılmazsa Serialization hatası verir
			var values = await _discountService.GetAllDiscountCouponAsync();
			return Ok(values);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetDiscountCouponById(int id)
		{
			var values = await _discountService.GetByIdDiscountCouponAsync(id);
			return Ok(values);
		}
        [HttpGet("GetCodeDetailByCode")]
        public async Task<IActionResult> GetCodeDetailByCode(string code)
		{
			var values = await _discountService.GetCodeDetailByCodeAsync(code);
			return Ok(values);
        }
        [HttpGet("GetDiscountCouponRate")]
        public IActionResult GetDiscountCouponRate(string code)
        {
            var values = _discountService.GetDiscountCouponRate(code);
            return Ok(values);
        }

        [HttpPost]
		public async Task<IActionResult> CreateDiscountCoupon(CreateDiscountCouponDto createCouponDto)
		{
			await _discountService.CreateDiscountCouponAsync(createCouponDto);
			return Ok("Kupon başarıyla oluşturuldu");
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteDiscountCoupon(int id)
		{
			await _discountService.DeleteDiscountCouponAsync(id);
			return Ok("Kupon başarıyla silindi");
		}

		[HttpPut]
		public async Task<IActionResult> UpdateDiscountCoupon(UpdateDiscountCouponDto updateCouponDto)
		{
			await _discountService.UpdateDiscountCouponAsync(updateCouponDto);
			return Ok("İndirim kuponu başarıyla güncellendi");
		}
	}
}
