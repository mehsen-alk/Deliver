using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;

namespace Persistence.Repositories;

public class ClientTripRepository : BaseRepository<Trip>, IClientTripRepository
{
    public ClientTripRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }
}