using Microsoft.EntityFrameworkCore;
using PagosCQRSDDDEventSourcing.Domain.Entities;

namespace PagosCQRSDDDEventSourcing.Infrastructure.Sql;

public class PagosDbContext : DbContext
{
    public PagosDbContext(DbContextOptions<PagosDbContext> options) : base(options) { }

    public DbSet<Pago> Pagos => Set<Pago>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pago>(builder =>
        {
            builder.OwnsOne(p => p.Monto, o =>
            {
                o.Property(m => m.Valor).HasColumnName("Monto");
            });
            builder.OwnsOne(p => p.MetodoPago, o =>
            {
                o.Property(m => m.Valor).HasColumnName("MetodoPago");
            });
            builder.HasKey(p => p.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}