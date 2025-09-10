using Deliver.Application.Features.Trips.Common.TripHistoryVm;
using MediatR;

namespace Deliver.Application.Features.Trips.AdminTrips.Query.FilterTrips;

public class FilterTripForAdminQuery : IRequest<List<TripHistoryVm>>
{
    public int Page { get; set; } = 1;
    public int? Status { get; set; } 
    public int? Id { get; set; } 
}