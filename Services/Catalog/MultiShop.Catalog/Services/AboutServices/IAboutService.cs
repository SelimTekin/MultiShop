using MultiShop.Catalog.Dtos.AboutDtos;
using MultiShop.Catalog.Dtos.BrandDtos;

namespace MultiShop.Catalog.Services.AboutServices
{
    public interface IAboutService
    {
        Task<List<ResultAboutDto>> GetAllAboutAsync();
        Task CreateAboutAsync(CreateAboutDto AboutDto);
        Task UpdateAboutAsync(UpdateAboutDto AboutDto);
        Task DeleteAboutAsync(string id);
        Task<GetByIdAboutDto> GetByIdAboutAsync(string id);
    }
}
