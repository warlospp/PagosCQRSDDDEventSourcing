using PagosCQRSDDDEventSourcing.Domain.Entities;

namespace PagosCQRSDDDEventSourcing.Infrastructure.Mongo;

public interface IMongoPagoRepository
{
    Task<PagoMongoDto?> GetByIdAsync(int id);
}