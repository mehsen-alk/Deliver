using Deliver.Application.Contracts.Persistence;
using Deliver.Domain.Entities;

namespace Persistence.Repositories;

public class RiderTripRepository : BaseRepository<Trip>, IRiderTripRepository
{
    public RiderTripRepository(DeliverDbContext dbContext) : base(dbContext)
    {
    }
}