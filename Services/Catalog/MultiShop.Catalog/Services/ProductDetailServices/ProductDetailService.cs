using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ProductDetailDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ProductDetailServices
{
	public class ProductDetailService : IProductDetailService
	{
		private readonly IMongoCollection<ProductDetail> _ProductDetailCollection; // ProductDetail collection'ı
		private readonly IMapper _mapper;

		public ProductDetailService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
			var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
			_ProductDetailCollection = database.GetCollection<ProductDetail>(_databaseSettings.ProductDetailCollectionName); // collection'ı aldık
			_mapper = mapper;
		}

		public async Task CreateProductDetailAsync(CreateProductDetailDto ProductDetailDto)
		{
			var value = _mapper.Map<ProductDetail>(ProductDetailDto); // ProductDetail entity'si ile parametreyi eşleştirdik
			await _ProductDetailCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
		}

		public async Task DeleteProductDetailAsync(string id)
		{
			await _ProductDetailCollection.DeleteOneAsync(x => x.ProductDetailID == id); // x -> _ProductDetailCollection
		}

		public async Task<List<ResultProductDetailDto>> GetAllProductDetailAsync()
		{
			var values = await _ProductDetailCollection.Find(x => true).ToListAsync(); // x=>true bütün değerleri getirir
			return _mapper.Map<List<ResultProductDetailDto>>(values); // ResultProductDetailDto'yu values ile map'ledik
		}

		public async Task<GetByIdProductDetailDto> GetByIdProductDetailAsync(string id)
		{
			var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductDetailID == id).FirstOrDefaultAsync();
			return _mapper.Map<GetByIdProductDetailDto>(values);
		}

        public async Task<GetByIdProductDetailDto> GetByProductIdProductDetailAsync(string id)
        {
            var values = await _ProductDetailCollection.Find<ProductDetail>(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<GetByIdProductDetailDto>(values);
        }

        public async Task UpdateProductDetailAsync(UpdateProductDetailDto updateProductDetailDto)
		{
			var values = _mapper.Map<ProductDetail>(updateProductDetailDto);

			// updateProductDetailDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
			await _ProductDetailCollection.FindOneAndReplaceAsync(x => x.ProductDetailID == updateProductDetailDto.ProductDetailID, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
		}
	}
}
