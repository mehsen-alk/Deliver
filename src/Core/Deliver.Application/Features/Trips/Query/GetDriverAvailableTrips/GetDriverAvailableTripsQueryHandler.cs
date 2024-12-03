using AutoMapper;
using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Contracts.Service;
using MediatR;

namespace Deliver.Application.Features.Trips.Query.GetDriverAvailableTrips;

public class GetDriverAvailableTripsQueryHandler
    : IRequestHandler<GetDriverAvailableTripsQuery, GetDriverAvailableTripsQueryVm>
{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly IMapper _mapper;
    private readonly IProfitService _profitService;

    public GetDriverAvailableTripsQueryHandler(
        IDriverTripRepository driverTripRepository,
        IMapper mapper,
        IProfitService profitService
    )
    {
        _driverTripRepository = driverTripRepository;
        _mapper = mapper;
        _profitService = profitService;
    }

    public async Task<GetDriverAvailableTripsQueryVm> Handle(
        GetDriverAvailableTripsQuery request,
        CancellationToken cancellationToken
    )
    {
        var trip =
            await _driverTripRepository.GetAvailableTrips(request.Page, request.Size);

        var tripsDto = _mapper.Map<List<TripDto>>(trip);

        foreach (var tripDto in tripsDto)
            tripDto.CaptainProfit =
                _profitService.GetCaptainProfitFromCalculatedDistance(
                    tripDto.CalculatedDistance
                );

        var vm = new GetDriverAvailableTripsQueryVm { Trips = tripsDto };

        return vm;
    }
}