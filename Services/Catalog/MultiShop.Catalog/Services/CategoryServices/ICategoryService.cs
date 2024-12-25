using MultiShop.Catalog.Dtos.CategoryDtos;

namespace MultiShop.Catalog.Services.CategoryServices
{
	public interface ICategoryService
	{
		Task<List<ResultCategoryDto>> GetAllCategoryAsync(); // Tüm kategorileri liste (liste ResultCategoryDto tipinde) şeklinde getirir
		Task CreateCategoryAsync(CreateCategoryDto categoryDto); // Kategori ekler
		Task UpdateCategoryAsync(UpdateCategoryDto categoryDto); // Kategori günceller
		Task DeleteCategoryAsync(string id);					// Kategori siler
		Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id); // id'ye göre kategori getirir
	}
}
