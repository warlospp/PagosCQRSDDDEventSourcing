using MediatR;
using PagosCQRSDDDEventSourcing.Domain.Entities;
using PagosCQRSDDDEventSourcing.Infrastructure.Kafka;
using PagosCQRSDDDEventSourcing.Infrastructure.Sql;

namespace PagosCQRSDDDEventSourcing.Application.Commands
{
    public class CreatePagoCommandHandler : IRequestHandler<CreatePagoCommand, int>
    {
        private readonly IPagoRepository _repository;
        private readonly KafkaProducer _kafka;
        

        public CreatePagoCommandHandler(IPagoRepository repository, KafkaProducer kafka)
        {
            _repository = repository;
            _kafka = kafka;
        }

        public async Task<int> Handle(CreatePagoCommand command, CancellationToken cancellationToken)
        {
            var monto = Monto.Crear(command.Monto);
            var metodoPago = MetodoPago.Crear(command.MetodoPago);
            var pago = new Pago(command.ClienteId, monto, metodoPago);
            var id = await _repository.AgregarAsync(pago);

            var dto = new PagoMongoDto
            {
                Id = pago.Id,
                ClienteId = pago.ClienteId,
                Monto = (double)pago.Monto.Valor,
                FechaPago = pago.FechaPago,
                MetodoPago = pago.MetodoPago.Valor,
                Estado = pago.Estado
            };
            
            await _kafka.SendMessageAsync(dto);
            return id;
        }
    }
}