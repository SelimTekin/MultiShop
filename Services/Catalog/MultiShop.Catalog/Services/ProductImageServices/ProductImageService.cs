using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
	public class ProductImageService : IProductImageService
	{
		private readonly IMongoCollection<ProductImage> _productImageCollection; // ProductImage collection'ı
		private readonly IMapper _mapper;

		public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
			var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
			_productImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName); // collection'ı aldık
			_mapper = mapper;
		}

		public async Task CreateProductImageAsync(CreateProductImageDto ProductImageDto)
		{
			var value = _mapper.Map<ProductImage>(ProductImageDto); // ProductImage entity'si ile parametreyi eşleştirdik
			await _productImageCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
		}

		public async Task DeleteProductImageAsync(string id)
		{
			await _productImageCollection.DeleteOneAsync(x => x.ProductImageID == id); // x -> _productImageCollection
		}

		public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
		{
			var values = await _productImageCollection.Find(x => true).ToListAsync(); // x=>true bütün değerleri getirir
			return _mapper.Map<List<ResultProductImageDto>>(values); // ResultProductImageDto'yu values ile map'ledik
		}

		public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
		{
			var values = await _productImageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync(); // <ProductImage> -> sınıfı için çalışacak anlamına geliyor
			return _mapper.Map<GetByIdProductImageDto>(values);
		}

        public async Task<GetByIdProductImageDto> GetByProductIdImageAsync(string id)
        {
            var values = await _productImageCollection.Find<ProductImage>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductImageDto>(values);
        }

        public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
		{
			var values = _mapper.Map<ProductImage>(updateProductImageDto);

			// updateProductImageDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
			await _productImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
		}
	}
}
