using Deliver.Domain.Entities;

namespace Deliver.Application.Contracts.Persistence;

public interface IClientTripRepository : IAsyncRepository<Trip>
{
}