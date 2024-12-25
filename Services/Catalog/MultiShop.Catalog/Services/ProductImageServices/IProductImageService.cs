using MultiShop.Catalog.Dtos.ProductImageDtos;

namespace MultiShop.Catalog.Services.ProductImageServices
{
	public interface IProductImageService
	{
		Task<List<ResultProductImageDto>> GetAllProductImageAsync(); // Tüm kategorileri liste (liste ResultProductImageDto tipinde) şeklinde getirir
		Task CreateProductImageAsync(CreateProductImageDto productImageDto); // Kategori ekler
		Task UpdateProductImageAsync(UpdateProductImageDto productImageDto); // Kategori günceller
		Task DeleteProductImageAsync(string id);                    // Kategori siler
		Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id); // id'ye göre kategori getirir
	}
}
