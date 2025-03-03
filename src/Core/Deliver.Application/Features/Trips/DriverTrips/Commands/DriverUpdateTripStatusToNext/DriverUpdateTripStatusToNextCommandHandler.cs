using Deliver.Application.Contracts.Persistence;
using Deliver.Application.Contracts.Service;
using Deliver.Application.Exceptions;
using Deliver.Domain.Entities;
using Deliver.Domain.Enums;
using MediatR;

namespace Deliver.Application.Features.Trips.DriverTrips.Commands.
    DriverUpdateTripStatusToNext;

public class DriverUpdateTripStatusToNextCommandHandler
    : IRequestHandler<DriverUpdateTripStatusToNextCommand, int>
{
    private readonly IDriverTripRepository _driverTripRepository;
    private readonly IAsyncRepository<Payment> _paymentRepository;
    private readonly IProfitService _profitService;
    private readonly ITripLogRepository _tripLogRepository;

    public DriverUpdateTripStatusToNextCommandHandler(
        IDriverTripRepository driverTripRepository,
        ITripLogRepository tripLogRepository,
        IAsyncRepository<Payment> paymentRepository,
        IProfitService profitService
    )
    {
        _driverTripRepository = driverTripRepository;
        _tripLogRepository = tripLogRepository;
        _paymentRepository = paymentRepository;
        _profitService = profitService;
    }

    public async Task<int> Handle(
        DriverUpdateTripStatusToNextCommand request,
        CancellationToken cancellationToken
    )
    {
        var driverTrip =
            await _driverTripRepository.GetCurrentTripAsync(request.DriverId);

        if (driverTrip == null)
            throw new DeliverException(DeliverErrorCodes.YouDontHaveAnActiveTrip);

        try
        {
            driverTrip.Status = driverTrip.Status.NextStatus();
        }
        catch
        {
            throw new DeliverException(
                DeliverErrorCodes.TripStatusCantBeUpdatedToNextStatus
            );
        }

        var log = new TripLog
        {
            Type = TripLogType.TripCancelledByDriver,
            Note = "trip updated by driver",
            Status = driverTrip.Status,
            TripId = driverTrip.Id
        };

        var transaction = await _driverTripRepository.BeginTransactionAsync();

        if (driverTrip.Status == TripStatus.Delivered)
        {
            var payment = new Payment
            {
                Type = PaymentType.Trip,
                Note = "trip cost",
                PaymentMethod = PaymentMethod.Cash,
                FromUserId = driverTrip.RiderId,
                ToUserId = (int)driverTrip.DriverId!,
                Status = PaymentStatus.Paid,
                TripId = driverTrip.Id,
                CompanyCommission = _profitService.CompanyCommission(),
                Amount = _profitService.GetTripCost(driverTrip.CalculatedDistance)
            };

            await _paymentRepository.AddAsync(payment);
        }

        try
        {
            await _driverTripRepository.UpdateAsync(driverTrip);
            await _tripLogRepository.AddAsync(log);

            await transaction.CommitAsync(cancellationToken);
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }

        return driverTrip.Id;
    }
}