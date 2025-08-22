using MultiShop.DtoLayer.OrderDtos;

namespace MultiShop.WebUI.Services.OrderServices
{
    public interface IOrderAddressService
    {
        Task CreateOrderAddressAsync(CreateOrderAddressDto createOrderAddressDto);
    }
}
