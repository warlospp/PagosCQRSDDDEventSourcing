using MediatR;
using PagosCQRSDDDEventSourcing.Domain.Entities;
using PagosCQRSDDDEventSourcing.Infrastructure.Mongo;


namespace PagosCQRSDDDEventSourcing.Application.Commands;

public class GetPagoByIdQueryHandler : IRequestHandler<GetPagoByIdQuery, PagoMongoDto?>
{
    private readonly IMongoPagoRepository _repository;

    public GetPagoByIdQueryHandler(IMongoPagoRepository repository)
    {
        _repository = repository;
    }

    public async Task<PagoMongoDto?> Handle(GetPagoByIdQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetByIdAsync(request.Id);
    }
}