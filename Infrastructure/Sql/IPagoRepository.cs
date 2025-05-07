using PagosCQRSDDDEventSourcing.Domain.Entities;


namespace PagosCQRSDDDEventSourcing.Infrastructure.Sql
{
    public interface IPagoRepository
    {
        Task<int> AgregarAsync(Pago pago);
        Task<IEnumerable<Pago>> ObtenerTodosAsync();
        Task<IEnumerable<Pago>> ObtenerPorClienteAsync(string clienteId);
    }
}