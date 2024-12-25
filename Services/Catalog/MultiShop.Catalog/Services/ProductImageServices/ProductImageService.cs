using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductImageDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductImageServices
{
	public class ProductImageService : IProductImageService
	{
		private readonly IMongoCollection<ProductImage> _ProductImageCollection; // ProductImage collection'ı
		private readonly IMapper _mapper;

		public ProductImageService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
			var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
			_ProductImageCollection = database.GetCollection<ProductImage>(_databaseSettings.ProductImageCollectionName); // collection'ı aldık
			_mapper = mapper;
		}

		public async Task CreateProductImageAsync(CreateProductImageDto ProductImageDto)
		{
			var value = _mapper.Map<ProductImage>(ProductImageDto); // ProductImage entity'si ile parametreyi eşleştirdik
			await _ProductImageCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
		}

		public async Task DeleteProductImageAsync(string id)
		{
			await _ProductImageCollection.DeleteOneAsync(x => x.ProductImageID == id); // x -> _ProductImageCollection
		}

		public async Task<List<ResultProductImageDto>> GetAllProductImageAsync()
		{
			var values = await _ProductImageCollection.Find(x => true).ToListAsync(); // x=>true bütün değerleri getirir
			return _mapper.Map<List<ResultProductImageDto>>(values); // ResultProductImageDto'yu values ile map'ledik
		}

		public async Task<GetByIdProductImageDto> GetByIdProductImageAsync(string id)
		{
			var values = await _ProductImageCollection.Find<ProductImage>(x => x.ProductImageID == id).FirstOrDefaultAsync(); // <ProductImage> -> sınıfı için çalışacak anlamına geliyor
			return _mapper.Map<GetByIdProductImageDto>(values);
		}

		public async Task UpdateProductImageAsync(UpdateProductImageDto updateProductImageDto)
		{
			var values = _mapper.Map<ProductImage>(updateProductImageDto);

			// updateProductImageDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
			await _ProductImageCollection.FindOneAndReplaceAsync(x => x.ProductImageID == updateProductImageDto.ProductImageID, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
		}
	}
}
