using MultiShop.Catalog.Dtos.ProductDetailDtos;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
	public interface IProductDetailService
	{
		Task<List<ResultProductDetailDto>> GetAllProductDetailAsync(); // Tüm kategorileri liste (liste ResultProductDetailDto tipinde) şeklinde getirir
		Task CreateProductDetailAsync(CreateProductDetailDto productDetailDto); // Kategori ekler
		Task UpdateProductDetailAsync(UpdateProductDetailDto productDetailDto); // Kategori günceller
		Task DeleteProductDetailAsync(string id);                    // Kategori siler
		Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id); // id'ye göre kategori getirir
	}
}
