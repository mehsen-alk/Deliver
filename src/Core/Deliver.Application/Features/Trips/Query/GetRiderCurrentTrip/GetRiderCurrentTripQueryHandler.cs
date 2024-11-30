using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetRiderCurrentTrip;

public class GetRiderCurrentTripQueryHandler
    : IRequestHandler<GetRiderCurrentTripQuery, RiderCurrentTripVm?>

{
    private readonly IMapper _mapper;
    private readonly IRiderTripRepository _riderTripRepository;

    public GetRiderCurrentTripQueryHandler(
        IMapper mapper,
        IRiderTripRepository riderTripRepository
    )
    {
        _mapper = mapper;
        _riderTripRepository = riderTripRepository;
    }

    public async Task<RiderCurrentTripVm?> Handle(
        GetRiderCurrentTripQuery request,
        CancellationToken cancellationToken
    )
    {
        var trip = await _riderTripRepository.GetCurrentTripAsync(request.RiderId);

        var vm = _mapper.Map<RiderCurrentTripVm>(trip);

        return vm;
    }
}