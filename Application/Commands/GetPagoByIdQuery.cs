using MediatR;
using PagosCQRSDDDEventSourcing.Domain.Entities;

namespace PagosCQRSDDDEventSourcing.Application.Commands;

public record GetPagoByIdQuery(int Id) : IRequest<PagoMongoDto?>;