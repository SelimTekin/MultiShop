using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.FeatureDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.FeatureServices
{
    public class FeatureService : IFeatureService
    {
        private readonly IMongoCollection<Feature> _featureCollection;
        private readonly IMapper _mapper;
        public FeatureService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
            _featureCollection = database.GetCollection<Feature>(_databaseSettings.FeatureCollectionName); // collection'ı aldık
            _mapper = mapper;
        }

        public async Task CreateFeatureAsync(CreateFeatureDto featureDto)
        {
            var value = _mapper.Map<Feature>(featureDto); // Feature entity'si ile parametreyi eşleştirdik
            await _featureCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
        }

        public async Task DeleteFeatureAsync(string id)
        {
            await _featureCollection.DeleteOneAsync(x => x.FeatureId == id); // x -> _featureCollection
        }

        public async Task<List<ResultFeatureDto>> GetAllFeatureAsync()
        {
            var values = await _featureCollection.Find(x => true).ToListAsync(); // x=>true bütün değerleri getirir
            return _mapper.Map<List<ResultFeatureDto>>(values); // ResultFeatureDto'yu values ile map'ledik
        }

        public async Task<GetByIdFeatureDto> GetByIdFeatureAsync(string id)
        {
            var values = await _featureCollection.Find<Feature>(x => x.FeatureId == id).FirstOrDefaultAsync(); // <Feature> -> sınıfı için çalışacak anlamına geliyor
            return _mapper.Map<GetByIdFeatureDto>(values);
        }

        public async Task UpdateFeatureAsync(UpdateFeatureDto updateFeatureDto)
        {
            var values = _mapper.Map<Feature>(updateFeatureDto);

            // updateFeatureDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
            await _featureCollection.FindOneAndReplaceAsync(x => x.FeatureId == updateFeatureDto.FeatureId, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
        }
    }
}
