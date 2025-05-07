using MediatR;
using PagosCQRSDDDEventSourcing.Domain.Entities;

namespace PagosCQRSDDDEventSourcing.Application.Commands
{
    public class CreatePagoCommand : IRequest<int>
    {
        public string ClienteId { get; set; }
        public decimal Monto { get; set; }
        public string MetodoPago { get; set; }
    }
}