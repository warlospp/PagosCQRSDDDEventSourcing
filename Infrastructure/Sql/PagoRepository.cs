using PagosCQRSDDDEventSourcing.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace PagosCQRSDDDEventSourcing.Infrastructure.Sql
{
    public class PagoRepository : IPagoRepository
    {
        private readonly PagosDbContext _context;

        public PagoRepository(PagosDbContext context)
        {
            _context = context;
        }

        public async Task<int> AgregarAsync(Pago pago)
        {
            _context.Pagos.Add(pago);
            await _context.SaveChangesAsync();
            return pago.Id;
        }

        public async Task<IEnumerable<Pago>> ObtenerTodosAsync()
        {
            return await _context.Pagos
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }

        public async Task<IEnumerable<Pago>> ObtenerPorClienteAsync(string clienteId)
        {
            return await _context.Pagos
                .Where(p => p.ClienteId == clienteId)
                .OrderByDescending(p => p.FechaPago)
                .ToListAsync();
        }
    }
}