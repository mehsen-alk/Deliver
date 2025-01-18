using AutoMapper;
using Deliver.Application.Exceptions;
using Deliver.Application.Features.Distance.Common.Location;
using MediatR;

namespace Deliver.Application.Features.Distance.Query.GetDistance;

public class GetDistanceQueryHandler : IRequestHandler<GetDistanceQuery, DistanceVm>
{
    private readonly IMapper _mapper;

    public GetDistanceQueryHandler(IMapper mapper)
    {
        _mapper = mapper;
    }

    public Task<DistanceVm> Handle(
        GetDistanceQuery request,
        CancellationToken cancellationToken
    )
    {
        var validator = new GetDistanceQueryValidator();
        var validationResults = validator.Validate(request);

        if (!validationResults.IsValid)
            throw new ValidationException(validationResults);

        var calculatedDistance = CalculateDistance(request.Origin, request.Destination);
        calculatedDistance = double.Ceiling(calculatedDistance);

        var calculatedDuration =
            (int)CalculateDurationInMinutes(calculatedDistance, 40.0);

        var vm = new DistanceVm
        {
            Distance = calculatedDistance,
            Duration = calculatedDuration,
            Price = (int)calculatedDistance * 11000
        };

        return Task.FromResult(vm);
    }

    /// <summary>
    ///     Calculates the distance between two geographic points using the Haversine formula.
    /// </summary>
    private double CalculateDistance(Location origin, Location destination)
    {
        const double r = 6371.0; // Earth's radius in kilometers

        // Convert degrees to radians
        var lat1Rad = ToRadians(origin.Lat);
        var lon1Rad = ToRadians(origin.Lon);
        var lat2Rad = ToRadians(destination.Lat);
        var lon2Rad = ToRadians(destination.Lon);

        // Calculate the differences
        var dLat = lat2Rad - lat1Rad;
        var dLon = lon2Rad - lon1Rad;

        // Apply the Haversine formula
        var a = Math.Pow(Math.Sin(dLat / 2), 2)
                + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(dLon / 2), 2);

        var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Calculate the distance in kilometers
        var distanceInKilometers = r * c;

        // Convert to the requested unit
        return distanceInKilometers;
    }

    /// <summary>
    ///     Converts degrees to radians.
    /// </summary>
    private static double ToRadians(double degrees)
    {
        return degrees * Math.PI / 180.0;
    }

    /// <summary>
    ///     Calculates the duration in minutes based on distance and speed.
    /// </summary>
    /// <param name="distanceInKm">The distance in kilometers.</param>
    /// <param name="speedInKmPerHour">The speed in kilometers per hour.</param>
    /// <returns>The duration in minutes.</returns>
    /// <exception cref="ArgumentException">Thrown if speed is less than or equal to zero.</exception>
    private double CalculateDurationInMinutes(
        double distanceInKm,
        double speedInKmPerHour
    )
    {
        if (speedInKmPerHour <= 0)
            throw new ArgumentException(
                "Speed must be greater than zero.",
                nameof(speedInKmPerHour)
            );

        // Calculate time in hours
        var timeInHours = distanceInKm / speedInKmPerHour;

        // Convert hours to minutes
        return timeInHours * 60;
    }
}