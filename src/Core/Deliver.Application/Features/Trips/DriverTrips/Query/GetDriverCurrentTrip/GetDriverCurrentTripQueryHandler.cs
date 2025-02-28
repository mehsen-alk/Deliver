using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Contracts.Service;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Query.GetDriverCurrentTrip;

public class GetDriverCurrentTripQueryHandler
    : IRequestHandler<GetDriverCurrentTripQuery, DriverCurrentTripVm?>

{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly IMapper _mapper;
    private readonly IProfitService _profitService;

    public GetDriverCurrentTripQueryHandler(
        IMapper mapper,
        IDriverTripRepository driverTripRepository,
        IProfitService profitService
    )
    {
        _mapper = mapper;
        _driverTripRepository = driverTripRepository;
        _profitService = profitService;
    }

    public async Task<DriverCurrentTripVm?> Handle(
        GetDriverCurrentTripQuery request,
        CancellationToken cancellationToken
    )
    {
        var trip = await _driverTripRepository.GetCurrentTripAsync(request.DriverId);

        var vm = _mapper.Map<DriverCurrentTripVm>(trip);

        if (vm == null) return null;

        vm.CaptainProfit =
            _profitService.GetCaptainProfitFromCalculatedDistance(vm.CalculatedDistance);

        return vm;
    }
}