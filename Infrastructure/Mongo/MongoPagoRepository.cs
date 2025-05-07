using MongoDB.Driver;
using PagosCQRSDDDEventSourcing.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace PagosCQRSDDDEventSourcing.Infrastructure.Mongo;

public class MongoPagoRepository : IMongoPagoRepository
{
    private readonly IMongoCollection<PagoMongoDto> _collection;

    public MongoPagoRepository(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["MongoDbSettings:ConnectionString"]);
        var database = client.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);
        _collection = database.GetCollection<PagoMongoDto>(configuration["MongoDbSettings:CollectionName"]);
    }

    public async Task<PagoMongoDto?> GetByIdAsync(int id)
    {
        return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
    }
}
