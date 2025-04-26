using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Services.BrandServices
{
    public interface IBrandService
    {
        Task<List<ResultBrandDto>> GetAllBrandAsync();
        Task CreateBrandAsync(CreateBrandDto BrandDto);
        Task UpdateBrandAsync(UpdateBrandDto BrandDto);
        Task DeleteBrandAsync(string id);
        Task<GetByIdBrandDto> GetByIdBrandAsync(string id);
    }
}
