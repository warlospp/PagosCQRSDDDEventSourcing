# PagosCQRSDDDEventSourcing

Este proyecto implementa una arquitectura basada en **DDD (Domain-Driven Design)** y **CQRS (Command Query Responsibility Segregation)** para la gestión de pagos, integrando persistencia transaccional en SQL Server, consulta en MongoDB y publicación de eventos en un tópico de **Confluent Kafka**.

---

## 🧱 Arquitectura

La solución está estructurada en capas independientes por responsabilidad:

- **Pagos.Domain**: contiene entidades, value objects y agregados de dominio.
- **Pagos.Application**: contiene los casos de uso (comandos y queries), interfaces de repositorio y lógica de negocio.
- **Pagos.Infrastructure**: contiene la implementación de persistencia en SQL Server y la publicación de eventos en Kafka.
- **Pagos.Infrastructure.NoSQL**: contiene la implementación de lectura desde MongoDB.
- **Pagos.API**: expone los endpoints HTTP para recibir comandos y ejecutar queries.
- **Pagos.Shared**: contiene modelos compartidos, contratos de mensajes, excepciones comunes, etc.

---

## ⚙️ Tecnologías

- [.NET 8](https://dotnet.microsoft.com/)
- **ASP.NET Core Web API**
- **Entity Framework Core** (SQL Server)
- **MongoDB.Driver** (MongoDB)
- **Confluent.Kafka** (.NET Kafka Client)
- **MediatR** (para orquestación de CQRS)
- **FluentValidation** (validación de entrada)
- **Swagger / Swashbuckle** (documentación interactiva)

---

## 🔄 Flujo de Datos

### Escritura (Command)
1. `POST /api/pagos` recibe un comando `CrearPagoCommand`.
2. Se procesa el comando y se guarda el pago en **SQL Server**.
3. Si la operación es exitosa, se publica un evento en **Kafka** (ej: `PagoCreadoEvent`).

### Lectura (Query)
1. `GET /api/pagos/{clienteId}` consulta pagos por cliente.
2. Los datos se recuperan desde **MongoDB**, optimizados para lectura.

---

## 📂 Estructura del Repositorio

│
├── Pagos.API/ # Web API (CQRS endpoints)
├── Pagos.Application/ # Casos de uso, comandos, queries
├── Pagos.Domain/ # Modelo de dominio (DDD)
├── Pagos.Infrastructure/ # SQL Server + Kafka + MongoDB (read side)


---

## 🚀 Endpoints REST

| Método | Ruta                      | Descripción                           |
|--------|---------------------------|---------------------------------------|
| POST   | `/api/pagos`              | Crear nuevo pago                      |
| GET    | `/api/pagos/{clienteId}`  | Obtener pagos por cliente desde MongoDB |

---

## 🛠️ Configuración

### Variables de entorno requeridas

- `ConnectionStrings__SQL`: conexión a SQL Server
- `ConnectionStrings__MongoDB`: conexión a MongoDB
- `Kafka__BootstrapServers`: servidores Kafka
- `Kafka__Topic`: nombre del tópico para eventos de pago

### Migraciones EF Core

```bash
cd Pagos.Infrastructure
dotnet ef migrations add InitialCreate -s ../Pagos.API
dotnet ef database update -s ../Pagos.API
