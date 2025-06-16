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
    private readonly ITripLogRepository _tripLogRepository;

    public GetDriverCurrentTripQueryHandler(
        IMapper mapper,
        IDriverTripRepository driverTripRepository,
        IProfitService profitService,
        ITripLogRepository tripLogRepository
    )
    {
        _mapper = mapper;
        _driverTripRepository = driverTripRepository;
        _profitService = profitService;
        _tripLogRepository = tripLogRepository;
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
            _profitService.GetCaptainProfit(
                _profitService.GetTripCost(vm.CalculatedDistance)
            );

        var acceptTripLog = await _tripLogRepository.GetAcceptedTripLogsAsync(vm.Id);

        if (acceptTripLog != null)
            vm.CanCancel =
                !((DateTime.UtcNow - acceptTripLog.CreatedDate).TotalMinutes > 5);
        else vm.CanCancel = false;

        return vm;
    }
}