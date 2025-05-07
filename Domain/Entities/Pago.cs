namespace PagosCQRSDDDEventSourcing.Domain.Entities
{

    public record Monto(decimal Valor)
    {
        public static Monto Crear(decimal valor)
        {
            if (valor <= 0)
                throw new ArgumentException("El monto debe ser mayor a 0.");
            return new Monto(valor);
        }
    }

    public record MetodoPago(string Valor)
    {
        public static MetodoPago Crear(string valor)
        {
            valor = valor.ToLower();
            if (valor != "tarjeta" && valor != "efectivo" && valor != "transferencia")
                throw new ArgumentException("Método de pago no válido.");
            return new MetodoPago(valor);
        }
    }

    public class Pago
    {
        public int Id { get; private set; }
        public string ClienteId { get; private set; }
        public Monto Monto { get; private set; }
        public DateTime FechaPago { get; private set; }
        public MetodoPago MetodoPago { get; private set; }
        public string Estado { get; private set; }

        private Pago() { } // EF Core

        public Pago(string clienteId, Monto monto, MetodoPago metodoPago)
        {
            ClienteId = clienteId;
            Monto = monto;
            MetodoPago = metodoPago;
            FechaPago = DateTime.UtcNow;
            Estado = "Procesado";
            ValidarMetodoPago(); 
        }

        public static Pago Crear(string clienteId, decimal monto, string metodoPago)
        {
            return new Pago(clienteId, new Monto(monto), new MetodoPago(metodoPago));
        }

        private void ValidarMetodoPago()
        {
            if (Monto.Valor > 100 && MetodoPago.Valor != "tarjeta")
                throw new InvalidOperationException("Pagos mayores a 100 solo pueden realizarse con tarjeta.");

            if (Monto.Valor <= 100 && MetodoPago.Valor != "efectivo" && MetodoPago.Valor != "transferencia")
                throw new InvalidOperationException("Pagos menores o iguales a 100 solo pueden ser en efectivo o transferencia.");
        }

        public void ReversarPago()
        {
            if (Estado == "Reversado")
                throw new InvalidOperationException("El pago ya está reversado.");

            Estado = "Reversado";
        }
        
    }

    public class PagoMongoDto
    {
        public int Id { get; set; }
        public string ClienteId { get; set; }
        public double Monto { get; set; }
        public DateTime FechaPago { get; set; }
        public string MetodoPago { get; set; }
        public string Estado { get; set; }
    }
}