using AutoMapper;
using MongoDB.Driver;
using MultiShop.Catalog.Dtos.ContactDtos;
using MultiShop.Catalog.Entities;
using MultiShop.Catalog.Settings;

namespace MultiShop.Catalog.Services.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly IMongoCollection<Contact> _contactCollection; // Contact collection'ı
        private readonly IMapper _mapper;

        public ContactService(IMapper mapper, IDatabaseSettings _databaseSettings)
        {
            var client = new MongoClient(_databaseSettings.ConnectionString); // bağlantı stringi
            var database = client.GetDatabase(_databaseSettings.DatabaseName); // veritabanını aldık
            _contactCollection = database.GetCollection<Contact>(_databaseSettings.ContactCollectionName); // collection'ı aldık
            _mapper = mapper;
        }

        public async Task CreateContactAsync(CreateContactDto ContactDto)
        {
            var value = _mapper.Map<Contact>(ContactDto); // Contact entity'si ile parametreyi eşleştirdik
            await _contactCollection.InsertOneAsync(value); // mongodb'de ekleme işlemi InsertOneAsync() metoduyla yapılıyor
        }

        public async Task DeleteContactAsync(string id)
        {
            await _contactCollection.DeleteOneAsync(x => x.ContactId == id); // x -> _contactCollection
        }

        public async Task<List<ResultContactDto>> GetAllContactAsync()
        {
            var values = await _contactCollection.Find(x => true).ToListAsync(); // x=>true bütün değerleri getirir
            return _mapper.Map<List<ResultContactDto>>(values); // ResultContactDto'yu values ile map'ledik
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var values = await _contactCollection.Find<Contact>(x => x.ContactId == id).FirstOrDefaultAsync(); // <Contact> -> sınıfı için çalışacak anlamına geliyor
            return _mapper.Map<GetByIdContactDto>(values);
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            var values = _mapper.Map<Contact>(updateContactDto);

            // updateContactDto'dan gelen id'ye eşit olan collection'daki değeri bul ve values parametresindeki değerle güncelle
            await _contactCollection.FindOneAndReplaceAsync(x => x.ContactId == updateContactDto.ContactId, values); // FindOneAndReplaceAsync mongodb'de güncelleme yapar
        }
    }
}
