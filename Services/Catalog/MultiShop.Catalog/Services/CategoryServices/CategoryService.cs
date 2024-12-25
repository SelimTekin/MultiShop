using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.CategoryDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.CategoryServices
{
	public class CategoryService : ICategoryService
	{
		private readonly IMongoCollection<Category> _categoryCollection; // category collection'ı
		private readonly IMapper _mapper;

		public CategoryService(IMapper mapper, IDatabaseSettings _databaseSettings)
		{
			var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
			var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
			_categoryCollection = database.GetCollection<Category>(_databaseSettings.CategoryCollectionName); // collection'ı aldık
			_mapper = mapper;
		}

		public async Task CreateCategoryAsync(CreateCategoryDto categoryDto)
		{
			var value = _mapper.Map<Category>(categoryDto); // Category entity'si ile parametreyi eşleştirdik
			await _categoryCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
		}

		public async Task DeleteCategoryAsync(string id)
		{
			await _categoryCollection.DeleteOneAsync(x => x.CategroyId == id); // x -> _categoryCollection
		}

		public async Task<List<ResultCategoryDto>> GetAllCategoryAsync()
		{
			var values = await _categoryCollection.Find(x=>true).ToListAsync(); // x=>true bütün değerleri getirir
			return _mapper.Map<List<ResultCategoryDto>>(values); // ResultCategoryDto'yu values ile map'ledik
		}

		public async Task<GetByIdCategoryDto> GetByIdCategoryAsync(string id)
		{
			var values = await _categoryCollection.Find<Category>(x => x.CategroyId == id).FirstOrDefaultAsync(); // <Category> -> sınıfı için çalışacak anlamına geliyor
			return _mapper.Map<GetByIdCategoryDto>(values);
		}

		public async Task UpdateCategoryAsync(UpdateCategoryDto updateCategoryDto)
		{
			var values = _mapper.Map<Category>(updateCategoryDto);

			// updateCategoryDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
			await _categoryCollection.FindOneAndReplaceAsync(x => x.CategroyId ==  updateCategoryDto.CategroyID, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
		}
	}
}
