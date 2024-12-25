using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductServices
{
	public class ProductService : IProductService
	{
		private readonly IMongoCollection<Product> _productCollection; // Product sınıfı için _productCollection field'ı örnekledik
		private readonly IMapper _mapper;

		public ProductService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
			var database = client.GetDatabase(_databaseSettings.DatabaseName);  // db
			_productCollection = database.GetCollection<Product>(_databaseSettings.ProductCollectionName);
			_mapper = mapper;
		}

		public async Task CreateProductAsync(CreateProductDto createProductDto)
		{
			var values = _mapper.Map<Product>(createProductDto);
			await _productCollection.InsertOneAsync(values);
		}

		public async Task DeleteProductAsync(string id)
		{
			await _productCollection.DeleteOneAsync(x => x.ProductId == id);
		}

		public async Task<List<ResultProductDto>> GetAllProductAsync()
		{
			var values = await _productCollection.Find<Product>(x => true).ToListAsync();
			return _mapper.Map<List<ResultProductDto>>(values);
		}

		public async Task<GetByIdProductDto> GetByIdProductAsync(string id)
		{
			var values = await _productCollection.Find<Product>(x => x.ProductId == id).FirstOrDefaultAsync();
			return _mapper.Map<GetByIdProductDto>(values);
		}

		public async Task UpdateProductAsync(UpdateProductDto updateProductDto)
		{
			var values = _mapper.Map<Product>(updateProductDto);
			await _productCollection.FindOneAndReplaceAsync(x => x.ProductId == updateProductDto.ProductId, values);
		}
	}
}
