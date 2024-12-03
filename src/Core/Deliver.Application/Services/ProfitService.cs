using Deliver.Application.Contracts.Service;

namespace Deliver.Application.Services;

public class ProfitService : IProfitService
{
    public double? GetCaptainProfitFromCalculatedDistance(double? distance)
    {
        if (distance == null) return null;

        var profit = distance * 4000;
        profit = Math.Round(profit ?? 0, 2);

        return profit;
    }
}