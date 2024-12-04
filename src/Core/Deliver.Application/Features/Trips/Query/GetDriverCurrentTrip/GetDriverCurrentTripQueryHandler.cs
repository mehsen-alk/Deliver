using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetDriverCurrentTrip;

public class GetDriverCurrentTripQueryHandler
    : IRequestHandler<GetDriverCurrentTripQuery, DriverCurrentTripVm?>

{
    private readonly IMapper _mapper;
    private readonly IRiderTripRepository _riderTripRepository;

    public GetDriverCurrentTripQueryHandler(
        IMapper mapper,
        IRiderTripRepository riderTripRepository
    )
    {
        _mapper = mapper;
        _riderTripRepository = riderTripRepository;
    }

    public async Task<DriverCurrentTripVm?> Handle(
        GetDriverCurrentTripQuery request,
        CancellationToken cancellationToken
    )
    {
        var trip = await _riderTripRepository.GetCurrentTripAsync(request.DriverId);

        var vm = _mapper.Map<DriverCurrentTripVm>(trip);

        return vm;
    }
}